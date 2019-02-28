using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetAllCommand
{
    public interface IPriceLevelInventoryItemGetAllCommand: ICommandAction<OperationResponse<IEnumerable<PriceLevelInventoryItemGetAllCommandOutputDTO>>>
    {
    }
}