using ApplicationLogic.Business.Commands.AppUserRole.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetAllCommand
{
    public interface IAppUserRoleGetAllCommand: ICommandAction<OperationResponse<IEnumerable<AppUserRoleGetAllCommandOutputDTO>>>
    {
    }
}