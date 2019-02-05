using ApplicationLogic.Business.Commands.InventoryAccount.DeleteCommand.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.DeleteCommand
{
    public interface IInventoryAccountDeleteCommand : ICommandFunc<int, OperationResponse<InventoryAccountDeleteCommandOutputDTO>>
    {
    }
}