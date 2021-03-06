using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
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
}
