using ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand
{
    public interface IInventoryItemGetByIdCommand: ICommandFunc<int, OperationResponse<InventoryItemGetByIdCommandOutputDTO>>
    {
    }
}