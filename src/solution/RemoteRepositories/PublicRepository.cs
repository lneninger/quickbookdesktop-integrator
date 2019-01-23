using ApplicationLogic.AppConfiguration;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Remote;
using Framework.Core.Messages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteRepositories
{
    public class PublicRepository : IPublicRepository
    {
        public PublicRepository(AppConfig appConfig)
        {
            this.RestSharpClient = new RestSharp.RestClient(appConfig.APIBaseURL);
        }

        public RestClient RestSharpClient { get; }

        public OperationResponse SendInventoryItem(IEnumerable<SyncInventoryItemsInputIventoryItemDTO> inventoryItemDTO)
        {
            var result = new OperationResponse();

            var request = new RestRequest("inventoryitem/sync", Method.POST);
            request.AddJsonBody(inventoryItemDTO);

            try
            {
                var response = this.RestSharpClient.Execute(request);
            }
            catch (Exception ex)
            {
                result.AddException($"Error sending sync call of Inventory items", ex);
            }

            return result;
        }
    }
}
