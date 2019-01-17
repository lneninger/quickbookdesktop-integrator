using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public interface ILogicalDeleteEntity
    {
        DateTime? DeletedAt { get; set; }

        bool? IsDeleted { get; set; }
    }
}
