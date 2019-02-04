using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand
{
    public class IncomeAccountUpdateCommand : AbstractDBCommand<DomainModel.IncomeAccount, IIncomeAccountDBRepository>, IIncomeAccountUpdateCommand
    {
        public IncomeAccountUpdateCommand(IDbContextScopeFactory dbContextScopeFactory, IIncomeAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IncomeAccountUpdateCommandOutputDTO> Execute(IncomeAccountUpdateCommandInputDTO input)
        {
            var result = new OperationResponse<IncomeAccountUpdateCommandOutputDTO>();
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
                        result.Bag = new IncomeAccountUpdateCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            Name = getByIdResult.Bag.Name,
                            FullName = getByIdResult.Bag.FullName
                        };
                    }

                }
            }

            return result;
        }
    }
}
