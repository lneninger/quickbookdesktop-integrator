using ApplicationLogic.Business.Commands.PriceLevel.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.InsertCommand
{
    public interface IPriceLevelInsertCommand: ICommandFunc<PriceLevelInsertCommandInputDTO, OperationResponse<PriceLevelInsertCommandOutputDTO>>
    {
    }
}