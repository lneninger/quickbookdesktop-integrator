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
using Framework.Logging.Log4Net;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.SyncInventoryItems
{
    public class SyncInventoryItemsCommand : BaseIoCDisposable, ISyncInventoryItemsCommand
    {
        protected LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SyncInventoryItemsCommand(IPublicRepository repository, IGetInventoryItemsCommand getInventoryItems, IGetPriceLevelsCommand getPriceLevels, IGetAccountByIdsCommand getAccountByIds)
        {
            this.Repository = repository;
            this.GetPriceLevels = getPriceLevels;
            this.GetInventoryItems = getInventoryItems;
            this.GetAccountByIds = getAccountByIds;
        }

        public IPublicRepository Repository { get; }
        public IGetInventoryItemsCommand GetInventoryItems { get; }
        public IGetPriceLevelsCommand GetPriceLevels { get; }
        public IGetAccountByIdsCommand GetAccountByIds { get; }

        public OperationResponse Execute()
        {
            var result = new OperationResponse();

            IEnumerable<GetInventoryItemsOutputIventoryItemDTO> items = null;
            IEnumerable<GetPriceLevelsOutputPriceLevelItemDTO> priceLevels = null;
            IEnumerable<GetAccountByIdsOutputDTO> accountsIncome = null;
            IEnumerable<GetAccountByIdsOutputDTO> accountsInventory = null;
            try
            {
                items = this.GetInventoryItems.Execute();

                priceLevels = this.GetPriceLevels.Execute();


                var accountIncomeIds = items.Where(item => !string.IsNullOrWhiteSpace(item.IncomeAccountId)).Select(item => item.IncomeAccountId).Distinct();
                accountsIncome = this.GetAccountByIds.Execute(accountIncomeIds.ToList());

                var accountAssetIds = items.Where(item => !string.IsNullOrWhiteSpace(item.AssetAccountId)).Select(item => item.IncomeAccountId).Distinct();
                accountsInventory = this.GetAccountByIds.Execute(accountAssetIds.ToList());

                Logger.Debug($"Success Retrieved Quickbooks data");

            }
            catch (Exception ex)
            {
                Logger.Error($"Error retrieving data from Quickbooks. {ex.Message}");
                result.AddException($"Error retrieving Quickbooks's Inventory items", ex);
            }

            if (result.IsSucceed)
            {
                var syncItems = new SyncInventoryItemsInputIventoryItemDTO
                {
                    InventoryItems = items,
                    PriceLevels = priceLevels,
                    AccountIncomes = accountsIncome,
                    AccountInventories = accountsInventory,
                };

                try
                {
                    result.AddResponse(this.Repository.SendInventoryItem(syncItems));
                    if (!result.IsSucceed)
                    {
                        Logger.Error($"Error sending data to public API");
                        string errorMessage = "";
                        result.Messages.Where(o => o.MessageType == MessageTypeEnum.Error).ToList().ForEach(error =>
                        {
                            Logger.Error(error.Message);
                            errorMessage += " " + errorMessage;
                        });
                        throw new Exception(result.Messages.ToList()[0].Message);
                    }

                    /*
                    int pagingSize = 20;
                    int index = 0;
                    SyncInventoryItemsInputIventoryItemDTO selectedItems = null;
                    do
                    {
                        selectedItems = new SyncInventoryItemsInputIventoryItemDTO
                        {
                            AccountIncomes = syncItems.AccountIncomes,
                            AccountInventories = syncItems.AccountInventories,
                            PriceLevels = syncItems.PriceLevels,
                            InventoryItems = syncItems.InventoryItems.OrderBy(item => item.Name).Skip(index * pagingSize).Take(pagingSize).ToList()
                        };

                        result.AddResponse(this.Repository.SendInventoryItem(selectedItems));
                        if (!result.IsSucceed)
                        {
                            Logger.Error($"Error sending data to public API");
                            result.Messages.Where(o => o.MessageType == MessageTypeEnum.Error).ToList().ForEach(error =>
                            {
                                Logger.Error(error.Message);
                                errorMessage += " " + errorMessage;
                            });

                            throw new Exception(errorMessage);
                        }

                        index++;
                    }
                    while (selectedItems.InventoryItems.Count() > 0);
                    */
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error sending data to public API. {ex.Message}");
                    result.AddException($"Error sync Quickbooks's Inventory items", ex);
                }
            }

            return result;
        }
    }
}
