using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    public class PagingResult<TClass>
    {
        public PagingResult()
        { }

        public PagingResult(int total, IList<TClass> items, bool success = true)
            : this()
        {
            this.Success = success;
            this.Items = items;
            this.Total = total;
        }

        public bool Success { get; set; }
        public int Total { get; set; }
        public IList<TClass> Items { get; set; }
    }
}
