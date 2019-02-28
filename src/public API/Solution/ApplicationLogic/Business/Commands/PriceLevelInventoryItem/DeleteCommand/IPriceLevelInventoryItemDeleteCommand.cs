using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.DeleteCommand.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.DeleteCommand
{
    public interface IPriceLevelInventoryItemDeleteCommand : ICommandFunc<int, OperationResponse<PriceLevelInventoryItemDeleteCommandOutputDTO>>
    {
    }
}