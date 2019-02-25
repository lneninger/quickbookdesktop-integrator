using ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand
{
    public interface IPriceLevelUpdateCommand: ICommandFunc<PriceLevelUpdateCommandInputDTO, OperationResponse<PriceLevelUpdateCommandOutputDTO>>
    {
    }
}