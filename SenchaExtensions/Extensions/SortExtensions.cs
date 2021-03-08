using System.Collections.Generic;

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
