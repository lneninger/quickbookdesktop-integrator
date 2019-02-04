using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand
{
    public class IncomeAccountPageQueryCommand : AbstractDBCommand<DomainModel.IncomeAccount, IIncomeAccountDBRepository>, IIncomeAccountPageQueryCommand
    {
        public IncomeAccountPageQueryCommand(IDbContextScopeFactory dbContextScopeFactory, IIncomeAccountDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<PageResult<IncomeAccountPageQueryCommandOutputDTO>> Execute(PageQuery<IncomeAccountPageQueryCommandInputDTO> input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.PageQuery(input);
            }
        }
    }
}
