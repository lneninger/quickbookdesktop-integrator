using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetByIdCommand.Models;
using Framework.Core.Messages;
using System.Linq;
using ApplicationLogic.Business.Commons.DTOs;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetByIdCommand
{
    public class PriceLevelInventoryItemGetByIdCommand : AbstractDBCommand<DomainModel.PriceLevelInventoryItem, IPriceLevelInventoryItemDBRepository>, IPriceLevelInventoryItemGetByIdCommand
    {

        public PriceLevelInventoryItemGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelInventoryItemDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelInventoryItemGetByIdCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<PriceLevelInventoryItemGetByIdCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    
                    result.Bag = new PriceLevelInventoryItemGetByIdCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        Name = getByIdResult.Bag.Name,
                        
                    };

                    
                }
            }

            return result;



        }
    }
}
