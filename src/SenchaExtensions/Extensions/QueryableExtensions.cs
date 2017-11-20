using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SenchaExtensions
{
    public static class QueryableExtensions
    {
        public static PagingResult<T> GetPagingResult<T>(this IQueryable<T> query,
            int page, int start, int limit, Sort sort)
                where T : class
        {
            return query.GetPagingResult<T>(page, start, limit, sort, null, null);
        }

        public static PagingResult<T> GetPagingResult<T>(this IQueryable<T> query,
            int page, int start, int limit, Sort sort, Filter filter, Group group)
                where T : class
        {
            sort = sort ?? throw new ArgumentNullException(nameof(sort));

            query = query
                .FilterBy(filter)
                .GroupBy(group)
                .SortBy(sort);

            return new PagingResult<T>()
            {
                Success = true,
                Total = query.Count(),
                Items = query
                    .Skip(start)
                    .Take(limit)
                    .ToList()
            };
        }

        public static IQueryable<T> FilterBy<T>(this IQueryable<T> query,
            IFilter filter) where T : class
        {
            if (filter != null)
            {
                foreach (var operation in filter.Operations)
                {
                    operation.Operator = operation.Operator ?? "eq";

                    if (PropertyExists<T>(operation.Property))
                    {
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var member = Expression.Property(parameter, operation.Property);
                        var constant = Expression.Constant(operation.GetFilterValue<T>());
                        var body = operation.AsExpression<T>(member, constant);
                        var expression = Expression.Lambda<Func<T, bool>>(body, parameter);

                        query = query.Where(expression);
                    }
                    else
                    {
                        throw new Exception($"Cannot find property {operation.Property} on {typeof(T)}");
                    }
                }
            }

            return query;
        }

        private static object GetFilterValue<T>(this IFilterOperation operation)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var property = properties.Find(operation.Property, true);
            var converter = TypeDescriptor.GetConverter(property.PropertyType);

            object value = null;

            var filterValueType = operation.Value.GetType();

            if (property.PropertyType != filterValueType)
            {
                if (filterValueType == typeof(Newtonsoft.Json.Linq.JArray))
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        value = JsonConvert.DeserializeObject<List<DateTime>>(operation.Value.ToString(),
                            new IsoDateTimeConverter() { DateTimeFormat = "yyyy-dd-MM" });
                    }
                    else if (property.PropertyType == typeof(Int32))
                    {
                        value = JsonConvert.DeserializeObject<List<Int32>>(operation.Value.ToString());
                    }
                    else if (property.PropertyType == typeof(Decimal))
                    {
                        value = JsonConvert.DeserializeObject<List<Decimal>>(operation.Value.ToString());
                    }
                }
                else
                {
                    value = converter.ConvertFromString(null, CultureInfo.InvariantCulture, operation.Value.ToString());
                }
            }

            return value = value ?? operation.Value;
        }

        public static IQueryable<T> GroupBy<T>(this IQueryable<T> query, IGroup group)
            where T : class
        {
            return query.SortBy<T>(group);
        }

        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, ISort sort)
            where T : class
        {
            if (sort != null)
            {
                foreach (var operation in sort.Operations)
                {
                    if (PropertyExists<T>(operation.Property))
                    {
                        query = operation.Direction == SortDirection.ASC
                         ? query = query.OrderByProperty(operation.Property)
                         : query = query.OrderByPropertyDescending(operation.Property);
                    }
                }
            }

            return query;
        }

        private static Expression Contains(Expression member, Expression expression)
        {
            MethodCallExpression contains = null;
            if (expression is ConstantExpression constant && constant.Value is IList && constant.Value.GetType().IsGenericType)
            {
                var type = constant.Value.GetType();
                var containsInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });
                contains = Expression.Call(constant, containsInfo, member);
            }

            return contains ?? Expression.Call(member, typeof(string).GetMethod("Contains"), expression); ;
        }

        private static Expression NotContains(Expression member, Expression expression)
        {
            return Expression.Not(Contains(member, expression));
        }

        private static IQueryable<T> OrderByProperty<T>(
            this IQueryable<T> source, string propertyName)
        {
            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod =
              OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

        private static IQueryable<T> OrderByPropertyDescending<T>(
            this IQueryable<T> source, string propertyName)
        {
            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod =
              OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

        private static Expression AsExpression<T>(this IFilterOperation operation,
            MemberExpression member, ConstantExpression constant)
        {
            switch (operation.AsOperatorEnum())
            {
                case Operator.GreaterThan:
                    return Expression.GreaterThan(member, constant);
                case Operator.GreaterOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);
                case Operator.LessThen:
                    return Expression.LessThan(member, constant);
                case Operator.LessOrEqual:
                    return Expression.LessThanOrEqual(member, constant);
                case Operator.Equal:
                    return Expression.Equal(member, constant);
                case Operator.NotEqual:
                    return Expression.NotEqual(member, constant);
                case Operator.In:
                case Operator.Like:
                    return Contains(member, constant);
                case Operator.NotIn:
                    return NotContains(member, constant);

            }

            throw new Exception($"Invalid operator {operation.AsOperatorEnum()} provided!");
        }

        private static Operator AsOperatorEnum(this IFilterOperation operation)
        {
            switch (operation.Operator)
            {
                case "eq":
                    return Operator.Equal;
                case "ne":
                    return Operator.NotEqual;
                case "gt":
                    return Operator.GreaterThan;
                case "ge":
                    return Operator.GreaterOrEqual;
                case "lt":
                    return Operator.LessThen;
                case "le":
                    return Operator.LessOrEqual;
                case "in":
                    return Operator.In;
                case "notin":
                    return Operator.NotIn;
                case "like":
                    return Operator.Like;
                default:
                    return Operator.None;
            }
        }

        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method =>
                method.Name == "OrderBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method =>
                method.Name == "OrderByDescending" && method.GetParameters().Length == 2);

        private static bool PropertyExists<T>(string propertyName)
        {
            return GetProperty<T>(propertyName) != null;
        }

        private static PropertyInfo GetProperty<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
