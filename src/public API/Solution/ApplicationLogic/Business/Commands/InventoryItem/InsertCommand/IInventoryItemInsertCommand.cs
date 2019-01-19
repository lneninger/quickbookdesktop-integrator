using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.InsertCommand
{
    public interface IInventoryItemInsertCommand: ICommandFunc<InventoryItemInsertCommandInputDTO, OperationResponse<InventoryItemInsertCommandOutputDTO>>
    {
    }
}