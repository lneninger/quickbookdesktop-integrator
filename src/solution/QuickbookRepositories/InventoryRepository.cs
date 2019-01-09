using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using QbSync.QbXml.Objects;
using QbSync.WebConnector.Core;
using QbSync.WebConnector.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbookRepositories
{
    public class InventoryRepository: IInventoryRepository
    {
        public void Request() {
        }

    }


    public class InventoryQueryWrapper {
       
            public const string NAME = "InventoryQuery";

            public class Request : StepQueryRequestWithIterator<QbSync.QbXml.Objects.ItemInventoryQueryRqType /**CustomerQueryRqType*/>
            {
                public override string Name => NAME;

                private readonly ApplicationDbContext dbContext;

                public Request(
                    ApplicationDbContext dbContext
                )
                {
                    this.dbContext = dbContext;
                }

                protected override Task<bool> ExecuteRequestAsync(IAuthenticatedTicket authenticatedTicket, ItemInventoryQueryRqType request)
                {
                    // By default, we return 100, let's do 5 here.
                    request.MaxReturned = "5";

                    return base.ExecuteRequestAsync(authenticatedTicket, request);
                }

                protected override async Task<string> RetrieveMessageAsync(IAuthenticatedTicket ticket, string key)
                {
                    var state = await dbContext.QbKvpStates
                        .Where(m => m.Ticket == ticket.Ticket)
                        .Where(m => m.CurrentStep == ticket.CurrentStep)
                        .Where(m => m.Key == key)
                        .FirstOrDefaultAsync();

                    return state?.Value;
                }
            }

            public class Response : StepQueryResponseWithIterator<ItemInventoryQueryRqType>
            {
                public override string Name => NAME;

                private readonly ApplicationDbContext dbContext;

                public Response(
                    ApplicationDbContext dbContext
                )
                {
                    this.dbContext = dbContext;
                }

                protected override Task ExecuteResponseAsync(IAuthenticatedTicket authenticatedTicket, ItemInventoryQueryRqType response)
                {
                    //if (response.CustomerRet != null)
                    if (response.IncludeRetElement != null)
                    {
                        foreach (var inventoryItem in response.IncludeRetElement)
                        //foreach (var customer in response.CustomerRet)
                        {
                            // save these custoemr.
                        }
                    }

                    return base.ExecuteResponseAsync(authenticatedTicket, response);
                }

                protected override async Task SaveMessageAsync(IAuthenticatedTicket ticket, string key, string value)
                {
                    var state = await dbContext.QbKvpStates
                        .Where(m => m.Ticket == ticket.Ticket)
                        .Where(m => m.CurrentStep == ticket.CurrentStep)
                        .Where(m => m.Key == key)
                        .FirstOrDefaultAsync();

                    if (state == null)
                    {
                        state = new QbKvpState
                        {
                            CurrentStep = ticket.CurrentStep,
                            Ticket = ticket.Ticket,
                            Key = key
                        };
                        dbContext.QbKvpStates.Add(state);
                    }

                    state.Value = value;

                    await dbContext.SaveChangesAsync();
                }
            }
        }

}
