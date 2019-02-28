using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.UpdateCommand
{
    public interface IPriceLevelInventoryItemUpdateCommand: ICommandFunc<PriceLevelInventoryItemUpdateCommandInputDTO, OperationResponse<PriceLevelInventoryItemUpdateCommandOutputDTO>>
    {
    }
}