using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class PriceLevel: AbstractBaseEntity, ILogicalDeleteEntity
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string PriceLevelType { get; set; }

        public bool IsActive { get; set; }

        public decimal? PriceLevelPercentage { get; set; }

        public virtual ICollection<PriceLevelInventoryItem> InventoryItems { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
