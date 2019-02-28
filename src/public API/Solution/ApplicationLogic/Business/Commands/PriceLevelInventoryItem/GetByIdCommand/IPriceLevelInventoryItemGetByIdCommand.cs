using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetByIdCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetByIdCommand
{
    public interface IPriceLevelInventoryItemGetByIdCommand: ICommandFunc<int, OperationResponse<PriceLevelInventoryItemGetByIdCommandOutputDTO>>
    {
    }
}