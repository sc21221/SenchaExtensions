using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    public static class SortExtensions
    {
        public static Sort Create(string property,
           SortDirection direction = SortDirection.ASC)
        {
            return new Sort()
            {
                Operations = new List<ISortOperation>()
                {
                    new SortOperation()
                    {
                        Property = property,
                        Direction = direction
                    }
                }
            };
        }
    }
}
