using System.Collections.Generic;

namespace SenchaExtensions
{
    public interface ISort
    {
        IList<ISortOperation> Operations { get; set; }
    }
}
