using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.DbContextImpl.Persistance
{
    public interface ITrackChangesEntity
    {
        DateTime? CreatedAt { get; set; }

        string CreatedBy { get; set; }

        DateTime? UpdatedAt { get; set; }

        string UpdatedBy { get; set; }
    }
}
