using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class InventoryItem: AbstractBaseEntity, ILogicalDeleteEntity
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public int LastIntegrationProcessId { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public decimal? Stock { get; set; }

        public string SalesDescription { get; set; }

        public decimal? SalesPrice { get; set; }

        public int? IncomeAccountId { get; set; }
        public virtual IncomeAccount IncomeAccount { get; set; }

        public int? AssetAccountId { get; set; }
        public virtual IncomeAccount AssetAccount { get; set; }


        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
