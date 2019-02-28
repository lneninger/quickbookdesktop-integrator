using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.DeleteCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.DeleteCommand
{
    public class PriceLevelInventoryItemDeleteCommand : AbstractDBCommand<DomainModel.PriceLevelInventoryItem, IPriceLevelInventoryItemDBRepository>, IPriceLevelInventoryItemDeleteCommand
    {
        public PriceLevelInventoryItemDeleteCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelInventoryItemDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelInventoryItemDeleteCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<PriceLevelInventoryItemDeleteCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    result.Bag = new PriceLevelInventoryItemDeleteCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        Name = getByIdResult.Bag.Name
                    };
                }

                var deleteResult = this.Repository.Delete(getByIdResult.Bag);
                result.AddResponse(deleteResult);
                if (result.IsSucceed)
                {
                    try
                    {
                        dbContextScope.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.AddException("Error deleting Product", ex);
                    }
                }
            }

            return result;
        }
    }
}
