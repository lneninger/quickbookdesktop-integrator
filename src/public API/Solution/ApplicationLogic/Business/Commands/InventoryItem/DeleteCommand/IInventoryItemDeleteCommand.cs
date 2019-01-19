using ApplicationLogic.Business.Commands.InventoryItem.DeleteCommand.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.DeleteCommand
{
    public interface IInventoryItemDeleteCommand : ICommandFunc<int, OperationResponse<InventoryItemDeleteCommandOutputDTO>>
    {
    }
}