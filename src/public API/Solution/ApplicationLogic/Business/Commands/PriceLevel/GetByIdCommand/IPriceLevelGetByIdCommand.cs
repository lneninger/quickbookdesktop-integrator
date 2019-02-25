using ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand
{
    public interface IPriceLevelGetByIdCommand: ICommandFunc<int, OperationResponse<PriceLevelGetByIdCommandOutputDTO>>
    {
    }
}