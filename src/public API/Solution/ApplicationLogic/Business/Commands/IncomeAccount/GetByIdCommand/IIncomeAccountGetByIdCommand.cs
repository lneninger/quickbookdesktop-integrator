using ApplicationLogic.Business.Commands.IncomeAccount.GetByIdCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.IncomeAccount.GetByIdCommand
{
    public interface IIncomeAccountGetByIdCommand: ICommandFunc<string, OperationResponse<IncomeAccountGetByIdCommandOutputDTO>>
    {
    }
}