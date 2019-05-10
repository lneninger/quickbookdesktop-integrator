using ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models;
using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand
{
    public class SyncFromDesktopCommand : AbstractDBCommand<DomainModel.InventoryItem, IInventoryItemDBRepository>, ISyncFromDesktopCommand
    {
        public IIncomeAccountDBRepository IncomeAccountRepository { get; }
        public IPriceLevelDBRepository PriceLevelRepository { get; }
        public IInventoryAccountDBRepository InventoryAccountRepository { get; }

        public SyncFromDesktopCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryItemDBRepository inventoryItemTepository, IIncomeAccountDBRepository incomeAccountRepository, IInventoryAccountDBRepository inventoryAccountRepository, IPriceLevelDBRepository priceLevelRepository) : base(dbContextScopeFactory, inventoryItemTepository)
        {
            this.InventoryAccountRepository = inventoryAccountRepository;
            this.IncomeAccountRepository = incomeAccountRepository;
            this.PriceLevelRepository = priceLevelRepository;
        }

        public OperationResponse Execute(SyncFromDesktopCommandInputDTO input)
        {
            var result = new OperationResponse<SyncFromDesktopCommandOutputDTO>();
            OperationResponse entityResponse = new OperationResponse();

            var syncIncomeAccountResponse = SyncIncomeAccounts(input.IntegrationProcessId, input.AccountIncomes);
            result.AddResponse(syncIncomeAccountResponse);

            var syncInventoryAccountResponse = SyncInventoryAccounts(input.IntegrationProcessId, input.AccountInventories);
            result.AddResponse(syncInventoryAccountResponse);

            var syncInventoryItemsResponse = SyncInventoryItems(input.IntegrationProcessId, input.InventoryItems);
            result.AddResponse(syncInventoryItemsResponse);



            var syncPriceLevelsResponse = SyncPriceLevels(input.IntegrationProcessId, input.PriceLevels);
            result.AddResponse(syncInventoryItemsResponse);

            return result;
        }

        private void MarkInventoryItemsAsDisabled(int integrationProcessId)
        {
            var result = new OperationResponse();

            var entityResponse = this.Repository.GetNotInIntegrationProcess(integrationProcessId);
            if (entityResponse.IsSucceed)
            {
                this.Repository.LogicalDelete(entityResponse.Bag.ToArray());
            }
        }

        private OperationResponse SyncInventoryItems(int integrationProcessId, IEnumerable<SyncFromDesktopCommandInputInventoryItemDTO> input)
        {
            var result = new OperationResponse();

            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                foreach (var item in input)
                {
                    var entityResponse = this.Repository.GetByExternalId(item.Id);
                    result.AddResponse(entityResponse);
                    var entity = entityResponse.Bag;
                    if (entity == null)
                    {
                        entity = new DomainModel.InventoryItem
                        {
                            ExternalId = item.Id,
                            LastIntegrationProcessId = integrationProcessId,
                            Name = item.Name,
                            FullName = item.FullName,
                            SalesPrice = (decimal?)item.SalePrice,
                            SalesDescription = item.SaleDescription,
                            Stock = (decimal?)item.Stock,
                            IncomeAccountId = this.IncomeAccountRepository.GetByExternalId(item.IncomeAccountId).Bag?.Id,
                            AssetAccountId = this.InventoryAccountRepository.GetByExternalId(item.AssetAccountId).Bag?.Id,
                        };

                        //SetIncomeAccount(entityResponse, item, entity);

                        try
                        {
                            var insertResult = this.Repository.Insert(entity);
                            result.AddResponse(insertResult);
                        }
                        catch (Exception ex)
                        {
                            result.AddError($"Error Adding Inventory Item {item.FullName}", ex);
                        }
                    }
                    else
                    {
                        entity.LastIntegrationProcessId = integrationProcessId;
                        entity.Name = item.Name;
                        entity.FullName = item.FullName;
                        entity.SalesPrice = (decimal?)item.SalePrice;
                        entity.SalesDescription = item.SaleDescription;
                        entity.Stock = (decimal?)item.Stock;
                        entity.IncomeAccountId = this.IncomeAccountRepository.GetByExternalId(item.IncomeAccountId).Bag?.Id;
                        entity.AssetAccountId = this.InventoryAccountRepository.GetByExternalId(item.AssetAccountId).Bag?.Id;
                        entity.IsDeleted = false;
                        entity.DeletedAt = null;
                        //SetIncomeAccount(entityResponse, item, entity);
                    }
                }

                try
                {
                    if (result.IsSucceed)
                    {
                        dbContextScope.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Error Adding Inventory Item", ex);
                }

                if (result.IsSucceed)
                {
                    MarkInventoryItemsAsDisabled(integrationProcessId);
                }
            }

            return result;
        }

        private OperationResponse SyncInventoryAccounts(int integrationProcessId, IEnumerable<SyncFromDesktopCommandInputAccountInventoryDTO> input)
        {
            var result = new OperationResponse();

            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                foreach (var item in input)
                {
                    var entityResponse = this.InventoryAccountRepository.GetByExternalId(item.Id);
                    result.AddResponse(entityResponse);

                    var entity = entityResponse.Bag;
                    if (entity == null)
                    {
                        entity = new DomainModel.InventoryAccount
                        {
                            LastIntegrationProcessId = integrationProcessId,
                            ExternalId = item.Id,
                            Name = item.Name,
                            FullName = item.FullName,
                            IsActive = item.IsActive,

                        };

                        try
                        {
                            var insertResult = this.InventoryAccountRepository.Insert(entity);
                            result.AddResponse(insertResult);
                        }
                        catch (Exception ex)
                        {
                            result.AddError($"Error Adding Inventory Account {item.FullName}", ex);
                        }
                    }
                    else
                    {
                        entity.LastIntegrationProcessId = integrationProcessId;
                        entity.Name = item.Name;
                        entity.FullName = item.FullName;
                        entity.IsActive = item.IsActive;
                    }
                }

                try
                {
                    if (result.IsSucceed)
                    {
                        dbContextScope.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Error Adding Product", ex);
                }
            }

            return result;
        }


        private OperationResponse SyncIncomeAccounts(int integrationProcessId, IEnumerable<SyncFromDesktopCommandInputAccountIncomeDTO> input)
        {
            var result = new OperationResponse();

            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                foreach (var item in input)
                {
                    var entityResponse = this.IncomeAccountRepository.GetByExternalId(item.Id);
                    result.AddResponse(entityResponse);

                    var entity = entityResponse.Bag;
                    if (entity == null)
                    {
                        entity = new DomainModel.IncomeAccount
                        {
                            LastIntegrationProcessId = integrationProcessId,
                            ExternalId = item.Id,
                            Name = item.Name,
                            FullName = item.FullName,
                            IsActive = item.IsActive,

                        };

                        try
                        {
                            var insertResult = this.IncomeAccountRepository.Insert(entity);
                            result.AddResponse(insertResult);
                        }
                        catch (Exception ex)
                        {
                            result.AddError($"Error Adding Income Account {item.FullName}", ex);
                        }
                    }
                    else
                    {
                        entity.LastIntegrationProcessId = integrationProcessId;
                        entity.Name = item.Name;
                        entity.FullName = item.FullName;
                        entity.IsActive = item.IsActive;
                    }
                }

                try
                {
                    if (result.IsSucceed)
                    {
                        dbContextScope.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Error Adding Product", ex);
                }
            }

            return result;
        }

        private OperationResponse SyncPriceLevels(int integrationProcessId, IEnumerable<SyncFromDesktopCommandInputPriceLevelDTO> input)
        {
            var result = new OperationResponse();

            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                foreach (var item in input)
                {
                    var entityResponse = this.PriceLevelRepository.GetByExternalId(item.Id);
                    result.AddResponse(entityResponse);
                    var entity = entityResponse.Bag;
                    if (entity == null)
                    {
                        entity = new DomainModel.PriceLevel
                        {
                            ExternalId = item.Id,
                            Name = item.Name,
                            IsActive = item.IsActive,
                            PriceLevelPercentage = item.PriceLevelPercentage,
                            PriceLevelType = item.PriceLevelType,
                            LastIntegrationProcessId = integrationProcessId
                        };

                        entity.InventoryItems = item.InventoryItems.Select(iItem => new DomainModel.PriceLevelInventoryItem
                        {
                            InventoryItemId = this.Repository.GetByExternalId(iItem.ItemId).Bag.Id,
                            PriceLevel = entity,
                            CustomPrice = iItem.CustomPrice,
                            CustomPricePercent = iItem.CustomPricePercent,
                            Type = iItem.Type,
                            LastIntegrationProcessId = integrationProcessId
                        }).ToList();
                        //SetIncomeAccount(entityResponse, item, entity);

                        try
                        {
                            var insertResult = this.PriceLevelRepository.Insert(entity);
                            result.AddResponse(insertResult);
                        }
                        catch (Exception ex)
                        {
                            result.AddError($"Error Adding Price Level {item.Name}", ex);
                        }
                    }
                    else
                    {
                        entity.LastIntegrationProcessId = integrationProcessId;
                        entity.Name = item.Name;
                        entity.Name = item.Name;
                        entity.IsActive = item.IsActive;
                        entity.PriceLevelPercentage = item.PriceLevelPercentage;
                        entity.PriceLevelType = item.PriceLevelType;

                        item.InventoryItems.ToList().ForEach(inventoryItem =>
                        {
                            var internalIventoryItem = this.Repository.GetByExternalId(inventoryItem.ItemId);
                            if (internalIventoryItem.IsSucceed && internalIventoryItem.Bag != null)
                            {
                                var existingInventoryItem = entity.InventoryItems.FirstOrDefault(o => o.InventoryItemId == internalIventoryItem.Bag.Id);
                                if (existingInventoryItem == null)
                                {
                                    entity.InventoryItems.Add(new DomainModel.PriceLevelInventoryItem
                                    {
                                        InventoryItemId = internalIventoryItem.Bag.Id,
                                        PriceLevel = entity,
                                        CustomPrice = inventoryItem.CustomPrice,
                                        CustomPricePercent = inventoryItem.CustomPricePercent,
                                        Type = inventoryItem.Type,
                                        LastIntegrationProcessId = integrationProcessId
                                    });
                                }
                                else
                                {
                                    existingInventoryItem.CustomPrice = inventoryItem.CustomPrice;
                                    existingInventoryItem.CustomPricePercent = inventoryItem.CustomPricePercent;
                                    existingInventoryItem.Type = inventoryItem.Type;
                                    existingInventoryItem.LastIntegrationProcessId = integrationProcessId;
                                }
                            }
                        });
                    }
                }

                try
                {
                    if (result.IsSucceed)
                    {
                        dbContextScope.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Error Adding Price Level", ex);
                }
            }

            return result;
        }

    }
}
