using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand
{
    public class PriceLevelGetAllCommand : AbstractDBCommand<DomainModel.PriceLevel, IPriceLevelDBRepository>, IPriceLevelGetAllCommand
    {
        public PriceLevelGetAllCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelDBRepository repository, IInventoryAccountDBRepository inventoryAccountRepository) : base(dbContextScopeFactory, repository)
        {
            this.InventoryAccountRepository = inventoryAccountRepository;
        }

        public IInventoryAccountDBRepository InventoryAccountRepository { get; }

        public OperationResponse<IEnumerable<PriceLevelGetAllCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<PriceLevelGetAllCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var inventoryAcountsResult = this.InventoryAccountRepository.GetAll();
                result.AddResponse(inventoryAcountsResult);
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new PriceLevelGetAllCommandOutputDTO
                    {
                        Id = entityItem.Id,
                        ExternalId = entityItem.ExternalId,
                        Name = entityItem.Name,
                        PriceLevelType = entityItem.PriceLevelType,
                        IsActive = entityItem.IsActive,
                        PriceLevelPercentage = entityItem.PriceLevelPercentage,
                        CreatedAt = entityItem.CreatedAt

                    }).ToList();

                    
                }
            }

            return result;
        }
    }
}
