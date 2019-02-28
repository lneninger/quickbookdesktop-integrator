using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.PageQueryCommand
{
    public interface IPriceLevelInventoryItemPageQueryCommand: ICommandFunc<PageQuery<PriceLevelInventoryItemPageQueryCommandInputDTO>, OperationResponse<PageResult<PriceLevelInventoryItemPageQueryCommandOutputDTO>>>
    {
    }
}