using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace SenchaExtensions
{
    [JsonObject]
    [TypeConverter(typeof(GroupConverter))]
    public class Group : IGroup
    {
        public IList<ISortOperation> Operations { get; set; }
    }
}
