using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.DbContextImpl.Persistance.Models.Sorting
{
    public class SortItem<T>
    {
        public string PropertyName { get; set; }

        public string SortOrder { get; set; }

        public dynamic SortExpression { get; set; }

        public bool Delete { get; set; }
    }
}
