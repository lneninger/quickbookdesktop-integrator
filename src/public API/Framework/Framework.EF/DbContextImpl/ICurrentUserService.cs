using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.DbContextImpl
{
    public interface ICurrentUserService: IDisposable
    {
        string CurrentUserId { get; set; }

    }
}
