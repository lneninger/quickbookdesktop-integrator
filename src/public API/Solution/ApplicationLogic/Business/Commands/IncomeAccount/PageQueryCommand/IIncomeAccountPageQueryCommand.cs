using ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand
{
    public interface IIncomeAccountPageQueryCommand: ICommandFunc<PageQuery<IncomeAccountPageQueryCommandInputDTO>, OperationResponse<PageResult<IncomeAccountPageQueryCommandOutputDTO>>>
    {
    }
}