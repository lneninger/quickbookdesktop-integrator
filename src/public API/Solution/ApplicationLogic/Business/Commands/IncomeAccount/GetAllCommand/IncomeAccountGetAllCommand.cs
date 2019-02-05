using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IncomeAccount.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.IncomeAccount.GetAllCommand
{
    public class IncomeAccountGetAllCommand : AbstractDBCommand<DomainModel.IncomeAccount, IIncomeAccountDBRepository>, IIncomeAccountGetAllCommand
    {
        public IncomeAccountGetAllCommand(IDbContextScopeFactory dbContextScopeFactory, IIncomeAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IEnumerable<IncomeAccountGetAllCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<IncomeAccountGetAllCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new IncomeAccountGetAllCommandOutputDTO
                    {
                        Id = entityItem.Id,
                        ExternalId = entityItem.ExternalId,
                        Name = entityItem.Name,
                        CreatedAt = entityItem.CreatedAt

                    }).ToList();
                }
            }

            return result;
        }
    }
}
