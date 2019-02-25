using ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand
{
    public interface IPriceLevelGetAllCommand: ICommandAction<OperationResponse<IEnumerable<PriceLevelGetAllCommandOutputDTO>>>
    {
    }
}