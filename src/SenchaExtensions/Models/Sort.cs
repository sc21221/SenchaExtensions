using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    [JsonObject]
    [TypeConverter(typeof(SortConverter))]
    public class Sort : ISort
    {
        public IList<ISortOperation> Operations { get; set; }
    }
}
