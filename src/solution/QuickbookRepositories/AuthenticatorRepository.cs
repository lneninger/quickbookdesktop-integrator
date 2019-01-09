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
        public AuthenticatorRepository()
        {
            //this.WebConnectorQwc = webConnectorQwc;
            //this.QbManager = qbManager;
        }

        public static Guid Ticket { get; private set; }
        public IWebConnectorQwc WebConnectorQwc { get; }
        public IQbManager QbManager { get; }

        public Task<IAuthenticatedTicket> GetAuthenticationFromLoginAsync(string login, string password)
        {
            AuthenticatorRepository.Ticket = Guid.NewGuid();
            IAuthenticatedTicket result = new AuthenticatedTicketDTO {
                Authenticated = true,
                Ticket = AuthenticatorRepository.Ticket.ToString()
            };
            return Task.FromResult(result);
        }

        public Task<IAuthenticatedTicket> GetAuthenticationFromTicketAsync(string ticket)
        {
            IAuthenticatedTicket result = new AuthenticatedTicketDTO
            {
                Authenticated = true,
                Ticket = AuthenticatorRepository.Ticket.ToString(),
            };
            return Task.FromResult(result);
        }

        public Task SaveTicketAsync(IAuthenticatedTicket ticket)
        {
            AuthenticatorRepository.Ticket = Guid.NewGuid();
            return Task.FromResult(true);
        }
    }
}
