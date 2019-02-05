using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Account : AbstractBaseEntity, ILogicalDeleteEntity
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public bool? IsActive { get; set; }

        public string AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
