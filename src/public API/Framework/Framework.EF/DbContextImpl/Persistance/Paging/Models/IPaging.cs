using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.DbContextImpl.Persistance.Paging.Models
{
    public interface IPaging
    {
        int PageSize { get; set; }

        int PageIndex { get; set; }
    }
}
