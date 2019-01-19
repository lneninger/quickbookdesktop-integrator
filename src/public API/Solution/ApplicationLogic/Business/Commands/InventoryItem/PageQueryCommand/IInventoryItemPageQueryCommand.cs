using ApplicationLogic.Business.Commands.InventoryItem.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.PageQueryCommand
{
    public interface IInventoryItemPageQueryCommand: ICommandFunc<PageQuery<InventoryItemPageQueryCommandInputDTO>, OperationResponse<PageResult<InventoryItemPageQueryCommandOutputDTO>>>
    {
    }
}