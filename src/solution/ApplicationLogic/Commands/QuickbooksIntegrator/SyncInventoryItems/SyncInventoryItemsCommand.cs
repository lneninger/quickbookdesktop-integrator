using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Remote;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems
{
    public class SyncInventoryItemsCommand : BaseIoCDisposable, ISyncInventoryItemsCommand
    {
        public SyncInventoryItemsCommand(IPublicRepository repository, IGetInventoryItemsCommand getInventoryItems)
        {
            this.Repository = repository;
            this.GetInventoryItems = getInventoryItems;
        }

        public IPublicRepository Repository { get; }
        public IGetInventoryItemsCommand GetInventoryItems { get; }

        public OperationResponse Execute()
        {
            var result = new OperationResponse();

            IEnumerable<GetInventoryItemsOutputIventoryItemDTO> items = null;
            try
            {
                items = this.GetInventoryItems.Execute();
            }
            catch (Exception ex)
            {
                result.AddException($"Error retrieving Quickbooks's Inventory items", ex);
            }

            if (result.IsSucceed)
            {
                var syncItems = items.Select(item => new SyncInventoryItemsInputIventoryItemDTO
                {
                    Name = item.Name,
                    FullName = item.FullName,
                    Cost = item.Cost,
                    Stock = item.Stock,
                    SaleDescription = item.SaleDescription,
                    SalePrice = item.SalePrice,
                });

                try
                {
                    result.AddResponse(this.Repository.SendInventoryItem(syncItems));
                }
                catch (Exception ex)
                {
                    result.AddException($"Error sync Quickbooks's Inventory items", ex);
                }
            }

            return result;
        }
    }
}
