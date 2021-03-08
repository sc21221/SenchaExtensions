using System.Collections.Generic;

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
