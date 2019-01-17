using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.DbContextImpl.Persistance.Paging.Models
{
    public class PageQuery<T>: BasePageQuery where T: BaseFilter
    {
        public T CustomFilter { get; set; }
    }
}
