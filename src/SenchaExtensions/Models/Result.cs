using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    public class PagingResult<T>
    {
        public PagingResult()
        { }

        public PagingResult(int total, IList<T> items, bool success = true)
            : this()
        {
            this.Success = success;
            this.Items = items;
            this.Total = total;
        }

        public bool Success { get; set; }
        public int Total { get; set; }
        public IList<T> Items { get; set; }
    }

    public class SaveResult
    {
        public SaveResult(int id, bool success = true)
        {
            Success = success;
            Items = new Items(id);
        }

        public bool Success { get; set; }
        public Items Items { get; set; }
    }

    public class Items
    {
        public Items(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
