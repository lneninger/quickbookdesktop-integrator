using ApplicationLogic.Business.Commands.InventoryItem.GetAllWithPriceLevelCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.GetAllWithPriceLevelCommand
{
    public interface IInventoryItemGetAllWithPriceLevelCommand: ICommandAction<OperationResponse<IEnumerable<InventoryItemGetAllWithPriceLevelCommandOutputDTO>>>
    {
    }
}