using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.UpdateCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.UpdateCommand
{
    public class PriceLevelInventoryItemUpdateCommand : AbstractDBCommand<DomainModel.PriceLevelInventoryItem, IPriceLevelInventoryItemDBRepository>, IPriceLevelInventoryItemUpdateCommand
    {
        public PriceLevelInventoryItemUpdateCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelInventoryItemDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelInventoryItemUpdateCommandOutputDTO> Execute(PriceLevelInventoryItemUpdateCommandInputDTO input)
        {
            var result = new OperationResponse<PriceLevelInventoryItemUpdateCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(input.Id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    getByIdResult.Bag.CustomPrice = input.CustomPrice;
                    
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
                        result.Bag = new PriceLevelInventoryItemUpdateCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            ExternalItemId = getByIdResult.Bag.InventoryItem.ExternalId,
                            CustomPrice = getByIdResult.Bag.CustomPrice,
                            CustomPricePercent = getByIdResult.Bag.CustomPricePercent
                        };
                    }

                }
            }

            return result;
        }
    }
}
