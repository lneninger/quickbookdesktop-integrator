using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Framework.SignalR
{
    public abstract class BaseHub<TClient> : Hub<TClient> where TClient: class
    {
        public override Task OnConnectedAsync()
        {
            string name = null;
            if (Context != null && Context.User != null && Context.User.Identity != null)
            {
                name = Context.User.Identity.Name;// ?? Context.User.Identity.GetUserId();
            }

/*
            if (name == null)
            {
                name = Context.QueryString["userId"];
            }
*/

            if (string.IsNullOrWhiteSpace(name))
            {
                name = this.GetUserName();
            }

            Groups.AddToGroupAsync(Context.ConnectionId, name);

            return base.OnConnectedAsync();
        }

        protected abstract string GetUserName();

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
    }
}
