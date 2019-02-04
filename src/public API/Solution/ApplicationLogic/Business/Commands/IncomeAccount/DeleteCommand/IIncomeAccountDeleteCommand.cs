using ApplicationLogic.Business.Commands.IncomeAccount.DeleteCommand.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.DeleteCommand
{
    public interface IIncomeAccountDeleteCommand : ICommandFunc<string, OperationResponse<IncomeAccountDeleteCommandOutputDTO>>
    {
    }
}