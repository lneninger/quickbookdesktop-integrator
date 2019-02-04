using ApplicationLogic.Business.Commands.IncomeAccount.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.GetAllCommand
{
    public interface IIncomeAccountGetAllCommand: ICommandAction<OperationResponse<IEnumerable<IncomeAccountGetAllCommandOutputDTO>>>
    {
    }
}