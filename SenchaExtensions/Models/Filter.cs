using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SenchaExtensions
{
    [JsonObject]
    [TypeConverter(typeof(FilterConverter))]
    public class Filter : IFilter
    {
        public IList<IFilterOperation> Operations { get; set; }
    }
}
