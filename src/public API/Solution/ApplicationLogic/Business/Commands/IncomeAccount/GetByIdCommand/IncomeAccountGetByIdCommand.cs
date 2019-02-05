using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IncomeAccount.GetByIdCommand.Models;
using Framework.Core.Messages;
using System.Linq;
using ApplicationLogic.Business.Commons.DTOs;

namespace ApplicationLogic.Business.Commands.IncomeAccount.GetByIdCommand
{
    public class IncomeAccountGetByIdCommand : AbstractDBCommand<DomainModel.IncomeAccount, IIncomeAccountDBRepository>, IIncomeAccountGetByIdCommand
    {

        public IncomeAccountGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, IIncomeAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IncomeAccountGetByIdCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<IncomeAccountGetByIdCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    
                    result.Bag = new IncomeAccountGetByIdCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        ExternalId = getByIdResult.Bag.ExternalId,
                        Name = getByIdResult.Bag.Name,
                        
                    };

                    
                }
            }

            return result;



        }
    }
}
