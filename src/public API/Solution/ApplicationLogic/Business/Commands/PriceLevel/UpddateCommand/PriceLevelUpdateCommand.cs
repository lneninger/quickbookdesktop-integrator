using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand
{
    public class PriceLevelUpdateCommand : AbstractDBCommand<DomainModel.PriceLevel, IPriceLevelDBRepository>, IPriceLevelUpdateCommand
    {
        public PriceLevelUpdateCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelUpdateCommandOutputDTO> Execute(PriceLevelUpdateCommandInputDTO input)
        {
            var result = new OperationResponse<PriceLevelUpdateCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(input.Id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    getByIdResult.Bag.Name = input.Name;
                    
                    try
                    {
                        dbContextScope.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.AddError("Error updating Inventory Item", ex);
                    }

                    getByIdResult = this.Repository.GetById(input.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new PriceLevelUpdateCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            Name = getByIdResult.Bag.Name
                        };
                    }

                }
            }

            return result;
        }
    }
}
