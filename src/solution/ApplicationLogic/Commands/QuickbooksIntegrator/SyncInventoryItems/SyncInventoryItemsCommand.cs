using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Remote;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds.Models;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems
{
    public class SyncInventoryItemsCommand : BaseIoCDisposable, ISyncInventoryItemsCommand
    {
        public SyncInventoryItemsCommand(IPublicRepository repository, IGetInventoryItemsCommand getInventoryItems, IGetAccountByIdsCommand getAccountByIds)
        {
            this.Repository = repository;
            this.GetInventoryItems = getInventoryItems;
            this.GetAccountByIds = getAccountByIds;
        }

        public IPublicRepository Repository { get; }
        public IGetInventoryItemsCommand GetInventoryItems { get; }
        public IGetAccountByIdsCommand GetAccountByIds { get; }

        public OperationResponse Execute()
        {
            var result = new OperationResponse();

            IEnumerable<GetInventoryItemsOutputIventoryItemDTO> items = null;
            IEnumerable<GetAccountByIdsOutputDTO> accounts = null;
            try
            {
                items = this.GetInventoryItems.Execute();

                var accountIncomeIds = items.Where(item => !string.IsNullOrWhiteSpace(item.IncomeAccountId)).Select(item => item.IncomeAccountId);
                var accountAssetIds = items.Where(item => !string.IsNullOrWhiteSpace(item.AssetAccountId)).Select(item => item.IncomeAccountId);

                var accountIds = accountAssetIds.Concat(accountIncomeIds).Distinct();
                accounts = this.GetAccountByIds.Execute(accountIds.ToList());
            }
            catch (Exception ex)
            {
                result.AddException($"Error retrieving Quickbooks's Inventory items", ex);
            }

            if (result.IsSucceed)
            {
                var syncItems = new SyncInventoryItemsInputIventoryItemDTO
                {
                    InventoryItems = items,
                    Accounts = accounts,
                };

               

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
