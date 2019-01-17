using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.DbContextImpl.Persistance.Paging.Models
{
    public class BasePageQuery: IPaging
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public Dictionary<string, string> Sort { get; set; }

    }
}
