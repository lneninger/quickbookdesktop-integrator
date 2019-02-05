using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IncomeAccount.DeleteCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.IncomeAccount.DeleteCommand
{
    public class IncomeAccountDeleteCommand : AbstractDBCommand<DomainModel.IncomeAccount, IIncomeAccountDBRepository>, IIncomeAccountDeleteCommand
    {
        public IncomeAccountDeleteCommand(IDbContextScopeFactory dbContextScopeFactory, IIncomeAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IncomeAccountDeleteCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<IncomeAccountDeleteCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    result.Bag = new IncomeAccountDeleteCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        ExternalId = getByIdResult.Bag.ExternalId,
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
