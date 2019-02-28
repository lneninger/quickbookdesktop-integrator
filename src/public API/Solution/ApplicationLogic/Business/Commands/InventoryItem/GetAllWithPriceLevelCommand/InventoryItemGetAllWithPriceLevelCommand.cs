using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryItem.GetAllWithPriceLevelCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetAllWithPriceLevelCommand
{
    public class InventoryItemGetAllWithPriceLevelCommand : AbstractDBCommand<DomainModel.InventoryItem, IInventoryItemDBRepository>, IInventoryItemGetAllWithPriceLevelCommand
    {
        public InventoryItemGetAllWithPriceLevelCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryItemDBRepository repository, IInventoryAccountDBRepository inventoryAccountRepository, IPriceLevelInventoryItemDBRepository priceLevelInventoryItemDBRepository) : base(dbContextScopeFactory, repository)
        {
            this.InventoryAccountDBRepository = inventoryAccountRepository;
            this.PriceLevelInventoryItemDBRepository = priceLevelInventoryItemDBRepository;
        }

        public IInventoryAccountDBRepository InventoryAccountDBRepository { get; }
        public IPriceLevelInventoryItemDBRepository PriceLevelInventoryItemDBRepository { get; }

        public OperationResponse<IEnumerable<InventoryItemGetAllWithPriceLevelCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<InventoryItemGetAllWithPriceLevelCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var inventoryAcountsResult = this.InventoryAccountDBRepository.GetAll();
                var priceLevelItemsResult = this.PriceLevelInventoryItemDBRepository.GetAll();
                result.AddResponse(inventoryAcountsResult);
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new InventoryItemGetAllWithPriceLevelCommandOutputDTO
                    {
                        Id = entityItem.Id,
                        ExternalId = entityItem.ExternalId,
                        Name = entityItem.Name,
                        AssetAccountId = entityItem.AssetAccountId, 
                        //InventoryAccountExternalId = inventoryAcountsResult.Bag.FirstOrDefault(invAccount => invAccount.Id == entityItem.AssetAccountId)?.ExternalId,// entityItem.AssetAccount?.ExternalId,
                        SaleDescription = entityItem.SalesDescription,
                        Price = entityItem.SalesPrice,
                        Stock = entityItem.Stock,
                        CreatedAt = entityItem.CreatedAt

                    }).ToList();

                    var inventoryAcounts = inventoryAcountsResult.Bag.ToList();
                    var priceLevelItems = priceLevelItemsResult.Bag.Where(o => o.PriceLevel.IsActive).ToList();
                    foreach (var item in result.Bag) {
                        var priceLevelSelectedItem = priceLevelItems.FirstOrDefault(priceLevelItem => priceLevelItem.InventoryItemId == item.Id);
                        if (priceLevelSelectedItem != null)
                        {
                            item.PriceLevelExternalId = priceLevelSelectedItem.PriceLevel.ExternalId;
                            item.PriceLevelCustomPrice = priceLevelSelectedItem.CustomPrice;
                        }
                    }
                }
            }

            return result;
        }
    }
}
