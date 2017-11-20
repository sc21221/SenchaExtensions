using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions.Tests
{
    public class User
    {
        public int Id { get; set; }
        public int OrigId { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Mark { get; set; }
        public bool IsBroker { get; set; }
        public bool IsManager { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public int OrdersSubmited { get; set; }
        public decimal AverageRate { get; set; }

        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }
    }
}
