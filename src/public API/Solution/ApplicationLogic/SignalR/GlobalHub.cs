using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Autofac;
using Framework.EF.DbContextImpl;
using Framework.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.SignalR
{
    public interface IGlobalHub {
        Task DataChanged(SignalREventArgs @event);
    }


    public class GlobalHub : Hub<IGlobalHub>// BaseSignalR<IGlobalHub>
    {
        public GlobalHub(/*ICurrentUserService currentUserService, IDbContextScopeFactory dbContextScopeFactory, IAppUserDBRepository repository*/) : base(/*currentUserService, dbContextScopeFactory, repository*/)
        {
        }

        public void SendMessageDataChanged(SignalREventArgs @event)
        {
            using (var scope = IoCGlobal.NewScope())
            {
                this.Clients.All.DataChanged(@event);
            }
        }

    }
}
