using ApplicationLogic.Interfaces.Repositories.Quickbook.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using QbSync.WebConnector.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbookRepositories
{
    public class AuthenticatorRepository : IAuthenticator
    {
        public AuthenticatorRepository(/*IWebConnectorQwc webConnectorQwc*/IQbManager qbManager)
        {
            //this.WebConnectorQwc = webConnectorQwc;
            this.QbManager = qbManager;
        }

        public IWebConnectorQwc WebConnectorQwc { get; }
        public IQbManager QbManager { get; }

        public Task<IAuthenticatedTicket> GetAuthenticationFromLoginAsync(string login, string password)
        {
            //this.WebConnectorQwc.GetQwcFile()
            var resultQB = this.QbManager.AuthenticateAsync(login, password);
            IAuthenticatedTicket result = new AuthenticatedTicketDTO();
            return Task.FromResult(result);
        }

        public Task<IAuthenticatedTicket> GetAuthenticationFromTicketAsync(string ticket)
        {
            throw new NotImplementedException();
        }

        public Task SaveTicketAsync(IAuthenticatedTicket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
