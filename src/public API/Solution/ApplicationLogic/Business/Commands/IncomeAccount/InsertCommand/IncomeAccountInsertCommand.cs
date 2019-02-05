using ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand.Models;
using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand
{
    public class IncomeAccountInsertCommand : AbstractDBCommand<DomainModel.IncomeAccount, IIncomeAccountDBRepository>, IIncomeAccountInsertCommand
    {
        public IncomeAccountInsertCommand(IDbContextScopeFactory dbContextScopeFactory, IIncomeAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IncomeAccountInsertCommandOutputDTO> Execute(IncomeAccountInsertCommandInputDTO input)
        {
            var result = new OperationResponse<IncomeAccountInsertCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var entity = new DomainModel.IncomeAccount
                {
                        Name = input.Name,
                    };
                

                try
                {
                    var insertResult = this.Repository.Insert(entity);
                    result.AddResponse(insertResult);
                    if (result.IsSucceed)
                    {
                        dbContextScope.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Error Adding Product", ex);
                }

                if (result.IsSucceed)
                {
                    var getByIdResult = this.Repository.GetById(entity.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new IncomeAccountInsertCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            ExternalId = getByIdResult.Bag.ExternalId,
                            Name = getByIdResult.Bag.Name
                        };
                    }

                }
            }

            return result;
        }
    }
}
