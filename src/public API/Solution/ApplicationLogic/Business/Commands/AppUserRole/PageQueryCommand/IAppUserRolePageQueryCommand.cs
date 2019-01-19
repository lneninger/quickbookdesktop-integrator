using ApplicationLogic.Business.Commands.AppUserRole.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.PageQueryCommand
{
    public interface IAppUserRolePageQueryCommand: ICommandFunc<PageQuery<AppUserRolePageQueryCommandInputDTO>, OperationResponse<PageResult<AppUserRolePageQueryCommandOutputDTO>>>
    {
    }
}