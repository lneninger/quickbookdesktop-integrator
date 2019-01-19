using ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand
{
    public interface IInventoryItemGetAllCommand: ICommandAction<OperationResponse<IEnumerable<InventoryItemGetAllCommandOutputDTO>>>
    {
    }
}