﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions
{
    public class SortOperation : ISortOperation
    {
        public string Property { get; set; }
        public SortDirection Direction { get; set; }
    }
}
