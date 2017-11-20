using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions.Tests
{
    public static class MockData
    {
        private static Random random = new Random();

        private static List<User> _users = new List<User>();
        private static List<Office> _offices = new List<Office>();

        static MockData()
        {
            #region mock

            for (int i = 0; i < 10; i++)
            {
                _offices.Add(new Office()
                {
                    Id = i,
                    Name = GetRandomString(5)
                });
            }

            for (int i = 0; i < 1000; i++)
            {
                var officeId = GetRandomInt(1, 10);

                _users.Add(new User()
                {
                    Id = i + 1,
                    OrigId = i + 1,
                    Login = $"ABC/{GetRandomString(5)}",
                    FirstName = GetRandomString(5),
                    LastName = GetRandomString(5),
                    Department = GetRandomString(3),
                    Email = $"{GetRandomString(6)}@mail.hr",
                    Mark = GetRandomString(4),
                    IsBroker = random.Next(0, 2) == 0 ? true : false,
                    IsManager = random.Next(0, 2) == 0 ? true : false,
                    Active = random.Next(0, 2) == 0 ? true : false,
                    DateCreated = GetRandomDate(),
                    OrdersSubmited = GetRandomInt(1, 100),
                    AverageRate = GetRandomDecimal(1, 5),
                    OfficeId = GetRandomInt(1, 10)
                });
            }

            _users.Add(new User()
            {
                Id = 9999,
                OrigId = 9999,
                Login = $"DZubak",
                FirstName = "Davor",
                LastName = "Zubak",
                Department = "Dev",
                Email = $"davor.zubak@mail.hr",
                Mark = "DZ01",
                IsBroker = true,
                IsManager = false,
                Active = true,
                DateCreated = new DateTime(2020,2,12),
                OrdersSubmited = 102,
                AverageRate = 5.2m,
                OfficeId = 7
            });

            _users.Add(new User()
            {
                Id = 8888,
                OrigId = 8888,
                Login = $"ABute",
                FirstName = "Ana",
                LastName = "Bute",
                Department = "Sale",
                Email = $"ana.bute@mail.hr",
                Mark = "AB01",
                IsBroker = false,
                IsManager = false,
                Active = true,
                DateCreated = new DateTime(1994, 10, 16),
                OrdersSubmited = -10,
                AverageRate = -5.2m,
                OfficeId = 8
            });

            #endregion mock
        }

        public static List<User> Users()
        {
            return _users;
        }

        public static List<Office> Offices()
        {
            return _offices;
        }

        #region Helpers
        private static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static T GetRandomEnum<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().OrderBy(e => Guid.NewGuid()).First();
        }

        private static DateTime GetRandomDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        private static int GetRandomInt(int from, int to)
        {
            return random.Next(from, to);
        }

        private static decimal GetRandomDecimal(decimal from, decimal to)
        {
            var next = (Decimal)random.NextDouble();

            return from + (next * (to - from));
        }
        #endregion Helpers
    }
}
