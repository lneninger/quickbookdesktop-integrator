using ApplicationLogic.Business.Commands.InventoryItem.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.UpdateCommand
{
    public interface IInventoryItemUpdateCommand: ICommandFunc<InventoryItemUpdateCommandInputDTO, OperationResponse<InventoryItemUpdateCommandOutputDTO>>
    {
    }
}