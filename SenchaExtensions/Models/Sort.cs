using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace SenchaExtensions
{
    [JsonObject]
    [TypeConverter(typeof(SortConverter))]
    public class Sort : ISort
    {
        public IList<ISortOperation> Operations { get; set; }
    }
}
