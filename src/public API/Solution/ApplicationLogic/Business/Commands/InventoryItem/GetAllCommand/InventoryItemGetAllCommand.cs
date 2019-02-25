using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand
{
    public class InventoryItemGetAllCommand : AbstractDBCommand<DomainModel.InventoryItem, IInventoryItemDBRepository>, IInventoryItemGetAllCommand
    {
        public InventoryItemGetAllCommand(IDbContextScopeFactory dbContextScopeFactory, IInventoryItemDBRepository repository, IInventoryAccountDBRepository inventoryAccountRepository) : base(dbContextScopeFactory, repository)
        {
            this.InventoryAccountRepository = inventoryAccountRepository;
        }

        public IInventoryAccountDBRepository InventoryAccountRepository { get; }

        public OperationResponse<IEnumerable<InventoryItemGetAllCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<InventoryItemGetAllCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var inventoryAcountsResult = this.InventoryAccountRepository.GetAll();
                result.AddResponse(inventoryAcountsResult);
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new InventoryItemGetAllCommandOutputDTO
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
                    foreach (var item in result.Bag) {
                        item.InventoryAccountExternalId = inventoryAcounts.FirstOrDefault(invAccount => invAccount.Id == item.AssetAccountId)?.ExternalId;// entityItem.AssetAccount?.ExternalId,

                    }
                }
            }

            return result;
        }
    }
}
