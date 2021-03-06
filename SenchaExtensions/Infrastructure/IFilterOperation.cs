using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    public interface IFilterOperation
    {
        string Property { get; set; }
        object Value { get; set; }
        string Operator { get; set; }
        bool ExactMatch { get; set; }
        bool AnyMatch { get; set; }
        bool CaseSensitive { get; set; }
    }
}
