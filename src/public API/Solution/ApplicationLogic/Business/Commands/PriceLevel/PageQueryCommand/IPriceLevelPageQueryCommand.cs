using ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand
{
    public interface IPriceLevelPageQueryCommand: ICommandFunc<PageQuery<PriceLevelPageQueryCommandInputDTO>, OperationResponse<PageResult<PriceLevelPageQueryCommandOutputDTO>>>
    {
    }
}