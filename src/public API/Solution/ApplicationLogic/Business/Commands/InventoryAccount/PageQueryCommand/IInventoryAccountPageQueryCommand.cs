using ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand
{
    public interface IInventoryAccountPageQueryCommand: ICommandFunc<PageQuery<InventoryAccountPageQueryCommandInputDTO>, OperationResponse<PageResult<InventoryAccountPageQueryCommandOutputDTO>>>
    {
    }
}