using ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand
{
    public interface IIncomeAccountInsertCommand: ICommandFunc<IncomeAccountInsertCommandInputDTO, OperationResponse<IncomeAccountInsertCommandOutputDTO>>
    {
    }
}