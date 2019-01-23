using ApplicationLogic.Business.Commands.InventoryItem.SyncCommand.Models;
using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.InventoryItem.SyncCommand
{
    public class InventoryItemSyncCommand : AbstractDBCommand<DomainModel.InventoryItem, IInventoryItemDBRepository>, IInventoryItemSyncCommand
    {
        public InventoryItemSyncCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryItemDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<InventoryItemSyncCommandOutputDTO> Execute(InventoryItemSyncCommandInputDTO input)
        {
            var result = new OperationResponse<InventoryItemSyncCommandOutputDTO>();
            bool add = false;
            OperationResponse<DomainModel.InventoryItem> entityResponse = null;
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                foreach (var item in input)
                {
                    entityResponse = this.Repository.GetByFullName(item.FullName);
                    var entity = entityResponse.Bag;
                    if (entity == null)
                    {
                        entity = new DomainModel.InventoryItem
                        {
                            Name = item.Name,
                            FullName = item.FullName,
                            SalesPrice = (decimal?)item.SalePrice,
                            SalesDescription = item.SaleDescription,
                            Stock = (decimal?)item.Stock,
                        };

                        try
                        {
                            var insertResult = this.Repository.Insert(entity);
                            result.AddResponse(insertResult);
                        }
                        catch (Exception ex)
                        {
                            result.AddError($"Error Adding Product {item.FullName}", ex);
                        }
                    }
                    else
                    {
                        entity.Name = item.Name;
                        entity.FullName = item.FullName;
                        entity.SalesPrice = (decimal?)item.SalePrice;
                        entity.SalesDescription = item.SaleDescription;
                        entity.Stock = (decimal?)item.Stock;
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
    }
}
