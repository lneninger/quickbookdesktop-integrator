using ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand
{
    public interface IIncomeAccountUpdateCommand: ICommandFunc<IncomeAccountUpdateCommandInputDTO, OperationResponse<IncomeAccountUpdateCommandOutputDTO>>
    {
    }
}