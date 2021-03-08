using System.Collections.Generic;

namespace SenchaExtensions
{
    public interface IFilter
    {
        IList<IFilterOperation> Operations { get; set; }
    }
}
