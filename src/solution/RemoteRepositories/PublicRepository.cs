﻿using ApplicationLogic.AppConfiguration;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Remote;
using Framework.Core.Messages;
using Framework.Logging.Log4Net;
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

        protected LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public RestClient RestSharpClient { get; }

        public OperationResponse SendInventoryItem(SyncInventoryItemsInputIventoryItemDTO inventoryItemDTO)
        {
            var result = new OperationResponse();

            var request = new RestRequest("syncfromdesktop/fromDesktop", Method.POST);
            request.AddJsonBody(inventoryItemDTO);

            try
            {
                var response = this.RestSharpClient.Execute(request);
                if (!response.IsSuccessful)
                {
                    result.AddError($"Error sending data to {this.RestSharpClient.BaseUrl}/{response.Request.Resource}");
                    result.AddError(response.ErrorMessage);
                    Logger.Error(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error sending sync call of Inventory items", ex);
            }

            return result;
        }
    }
}
