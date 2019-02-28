using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand
{
    public interface IPriceLevelInventoryItemInsertCommand: ICommandFunc<PriceLevelInventoryItemInsertCommandInputDTO, OperationResponse<PriceLevelInventoryItemInsertCommandOutputDTO>>
    {
    }
}