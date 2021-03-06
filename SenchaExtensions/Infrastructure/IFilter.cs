using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    public interface IFilter
    {
        IList<IFilterOperation> Operations { get; set; }
    }
}
