using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand
{
    public class PriceLevelDeleteCommand : AbstractDBCommand<DomainModel.PriceLevel, IPriceLevelDBRepository>, IPriceLevelDeleteCommand
    {
        public PriceLevelDeleteCommand(IDbContextScopeFactory dbContextScopeFactory, IPriceLevelDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PriceLevelDeleteCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<PriceLevelDeleteCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    result.Bag = new PriceLevelDeleteCommandOutputDTO
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
