using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace SenchaExtensions
{
    [JsonObject]
    [TypeConverter(typeof(FilterConverter))]
    public class Filter : IFilter
    {
        public IList<IFilterOperation> Operations { get; set; }
    }
}
