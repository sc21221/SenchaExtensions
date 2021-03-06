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
    [TypeConverter(typeof(GroupConverter))]
    public class Group : IGroup
    {
        public IList<ISortOperation> Operations { get; set; }
    }
}
