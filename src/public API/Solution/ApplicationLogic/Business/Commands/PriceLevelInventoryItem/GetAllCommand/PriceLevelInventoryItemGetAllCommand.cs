using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetAllCommand
{
    public class PriceLevelInventoryItemGetAllCommand : AbstractDBCommand<DomainModel.PriceLevelInventoryItem, IPriceLevelInventoryItemDBRepository>, IPriceLevelInventoryItemGetAllCommand
    {
        public PriceLevelInventoryItemGetAllCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelInventoryItemDBRepository repository, IInventoryAccountDBRepository inventoryAccountRepository) : base(dbContextScopeFactory, repository)
        {
            this.InventoryAccountRepository = inventoryAccountRepository;
        }

        public IInventoryAccountDBRepository InventoryAccountRepository { get; }

        public OperationResponse<IEnumerable<PriceLevelInventoryItemGetAllCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<PriceLevelInventoryItemGetAllCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var inventoryAcountsResult = this.InventoryAccountRepository.GetAll();
                result.AddResponse(inventoryAcountsResult);
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new PriceLevelInventoryItemGetAllCommandOutputDTO
                    {
                        Id = entityItem.Id,

                    }).ToList();

                    
                }
            }

            return result;
        }
    }
}
