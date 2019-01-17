using Framework.Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.DbContextImpl
{
    public class CurrentUserService : BaseIoCDisposable, ICurrentUserService
    {
        private string _currentUserId;
        public string CurrentUserId
        {
            get
            {
                return this._currentUserId;
            }
            set
            {
                this._currentUserId = value;
            }
        }
    }
}
