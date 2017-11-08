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
    public class Filter
    {
        public IList<FilterOperation> Operations { get; set; }
    }

    public class FilterOperation
    {
        public string Property { get; set; }
        public object Value { get; set; }
        public string Operator { get; set; }
        public bool ExactMatch { get; set; }
        public bool AnyMatch { get; set; }
        public bool CaseSensitive { get; set; }
    }
}
