using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace SenchaExtensions
{
    public static class Extensions
    {
        public static PagingResult<T> GetPagingResult<T>(this IQueryable<T> query,
            int page, int start, int limit)
        {
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

        public static PagingResult<T> GetPagingResult<T>(this IQueryable<T> query,
            int page, int start, int limit, Sort sort, Filter filter, Group group)
        {
            query = query
                .FilterBy(filter)
                .GroupBy(group)
                .SortBy(sort);

            return query.GetPagingResult<T>(page, start, limit);
        }

        public static IQueryable<T> FilterBy<T>(this IQueryable<T> query, Filter filter)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));

            if (filter != null)
            {
                foreach (var operation in filter.Operations)
                {
                    var prop = props.Find(operation.Property, true);

                    if (prop == null)
                    {
                        throw new Exception($"Cannot find property {operation.Property} on {typeof(T)}");
                    }

                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);

                    try
                    {
                        object value = null;

                        var filterValueType = operation.Value.GetType();

                        if (prop.PropertyType != filterValueType)
                        {
                            if (filterValueType == typeof(Newtonsoft.Json.Linq.JArray))
                            {
                                if (prop.PropertyType == typeof(DateTime))
                                {
                                    var format = "yyyy-dd-MM";
                                    var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

                                    value = JsonConvert.DeserializeObject<DateTime[]>(operation.Value.ToString(), dateTimeConverter);
                                }
                                else if(prop.PropertyType == typeof(Int32))
                                {
                                    value = JsonConvert.DeserializeObject<Int32[]>(operation.Value.ToString());
                                }
                                else if(prop.PropertyType == typeof(Double))
                                {
                                    value = JsonConvert.DeserializeObject<Double[]>(operation.Value.ToString());
                                }
                            }
                            else
                            {
                                value = converter.ConvertFromString(null, CultureInfo.InvariantCulture, operation.Value.ToString());
                            }
                        }
                        else
                        {
                            value = operation.Value;
                        }

                        if (operation.Operator == null)
                        {
                            query = query.Where(x => prop.GetValue(x).Equals(value));
                        }
                        else
                        {
                            var _operator = operation.Operator.AsEnum<Operator>();

                            if (prop.PropertyType == typeof(DateTime))
                            {
                                if (_operator == Operator.GreaterThan)
                                {
                                    query = query.Where(x => ((DateTime)prop.GetValue(x)).Date > ((DateTime)value).Date);
                                }
                                if (_operator == Operator.GreaterOrEqual)
                                {
                                    query = query.Where(x => ((DateTime)prop.GetValue(x)).Date >= ((DateTime)value).Date);
                                }
                                else if (_operator == Operator.LessThen)
                                {
                                    query = query.Where(x => ((DateTime)prop.GetValue(x)).Date < ((DateTime)value).Date);
                                }
                                else if (_operator == Operator.LessOrEqual)
                                {
                                    query = query.Where(x => ((DateTime)prop.GetValue(x)).Date <= ((DateTime)value).Date);
                                }
                                else if (_operator == Operator.Equal)
                                {
                                    query = query.Where(x => ((DateTime)prop.GetValue(x)).Date == ((DateTime)value).Date);
                                }
                                else if (_operator == Operator.NotEqual)
                                {
                                    query = query.Where(x => ((DateTime)prop.GetValue(x)).Date != ((DateTime)value).Date);
                                }
                                else if (_operator == Operator.In)
                                {
                                    query = query.Where(x => ((DateTime[])value).Contains(((DateTime)prop.GetValue(x)).Date));
                                }
                                else if (_operator == Operator.NotIn)
                                {
                                    query = query.Where(x => !((DateTime[])value).Contains(((DateTime)prop.GetValue(x)).Date));
                                }
                            }
                            else if (prop.PropertyType == typeof(Int32))
                            {
                                if (_operator == Operator.GreaterThan)
                                {
                                    query = query.Where(x => (Int32)prop.GetValue(x) > (Int32)value);
                                }
                                else if (_operator == Operator.GreaterOrEqual)
                                {
                                    query = query.Where(x => (Int32)prop.GetValue(x) >= (Int32)value);
                                }
                                else if (_operator == Operator.LessThen)
                                {
                                    query = query.Where(x => (Int32)prop.GetValue(x) < (Int32)value);
                                }
                                else if (_operator == Operator.LessOrEqual)
                                {
                                    query = query.Where(x => (Int32)prop.GetValue(x) <= (Int32)value);
                                }
                                else if (_operator == Operator.Equal)
                                {
                                    query = query.Where(x => (Int32)prop.GetValue(x) == (Int32)value);
                                }
                                else if (_operator == Operator.NotEqual)
                                {
                                    query = query.Where(x => (Int32)prop.GetValue(x) != (Int32)value);
                                }
                                else if (_operator == Operator.In)
                                {
                                    query = query.Where(x => ((Int32[])value).Contains((Int32)prop.GetValue(x)));
                                }
                                else if (_operator == Operator.NotIn)
                                {
                                    query = query.Where(x => !((Int32[])value).Contains((Int32)prop.GetValue(x)));
                                }
                            }
                            else if (prop.PropertyType == typeof(Double))
                            {
                                if (_operator == Operator.GreaterThan)
                                {
                                    query = query.Where(x => (Double)prop.GetValue(x) > (Double)value);
                                }
                                else if (_operator == Operator.GreaterOrEqual)
                                {
                                    query = query.Where(x => (Double)prop.GetValue(x) >= (Double)value);
                                }
                                else if (_operator == Operator.LessThen)
                                {
                                    query = query.Where(x => (Double)prop.GetValue(x) < (Double)value);
                                }
                                else if (_operator == Operator.LessOrEqual)
                                {
                                    query = query.Where(x => (Double)prop.GetValue(x) <= (Double)value);
                                }
                                else if (_operator == Operator.Equal)
                                {
                                    query = query.Where(x => Math.Round((Double)prop.GetValue(x), 2) == ((Double)value));
                                }
                                else if (_operator == Operator.NotEqual)
                                {
                                    query = query.Where(x => Math.Round((Double)prop.GetValue(x), 2) != ((Double)value));
                                }
                                else if (_operator == Operator.In)
                                {
                                    query = query.Where(x => ((Double[])value).Contains(Math.Round((Double)prop.GetValue(x), 2)));
                                }
                                else if (_operator == Operator.NotIn)
                                {
                                    query = query.Where(x => !((Double[])value).Contains(Math.Round((Double)prop.GetValue(x), 2)));
                                }
                            }
                            else if (prop.PropertyType == typeof(Boolean))
                            {
                                query = query.Where(x => (Boolean)prop.GetValue(x) == (Boolean)value);
                            }
                            else
                            {
                                query = query.Where(x => prop.GetValue(x).ToString().Contains(value.ToString()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return query;
        }

        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, ISortable sort)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));

            if (sort != null)
            {
                foreach (var operation in sort.Operations)
                {
                    var prop = TypeDescriptor.GetProperties(typeof(T)).Find(operation.Property, true);

                    query = operation.Direction == SortDirection.ASC
                        ? query.OrderBy(x => prop.GetValue(x))
                        : query.OrderByDescending(x => prop.GetValue(x));
                }
            }

            return query;
        }

        public static IQueryable<T> GroupBy<T>(this IQueryable<T> query, Group group)
        {
            return query.SortBy<T>(group);
        }

        internal static Operator AsEnum<T>(this string value)
        {
            switch (value)
            {
                case "eq":
                    return Operator.Equal;
                case "ne":
                    return Operator.Equal;
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
    }
}
