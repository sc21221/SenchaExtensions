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
    public class Sort : ISortable
    {
        public IList<SortOperation> Operations { get; set; }
    }

    public class SortOperation
    {
        public string Property { get; set; }
        public SortDirection Direction { get; set; }
    }

    public enum SortDirection
    {
        ASC,
        DESC
    }
}
