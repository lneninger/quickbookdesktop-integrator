//using QbSync.WebConnector.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces.Repositories.Quickbook.Models
{
    public class AuthenticatedTicketDTO : IAuthenticatedTicket
    {
        public string Ticket { get; set; }

        public string CurrentStep { get; set; }

        public bool Authenticated { get; set; }
    }
}
