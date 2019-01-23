using ApplicationLogic.Business.Commands.InventoryItem.SyncCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.SyncCommand
{
    public interface IInventoryItemSyncCommand: ICommandFunc<InventoryItemSyncCommandInputDTO, OperationResponse<InventoryItemSyncCommandOutputDTO>>
    {
    }
}