using ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand
{
    public interface IPriceLevelDeleteCommand : ICommandFunc<int, OperationResponse<PriceLevelDeleteCommandOutputDTO>>
    {
    }
}