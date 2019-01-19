using ApplicationLogic.Business.Commands.AppUserRole.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.InsertCommand
{
    public interface IAppUserRoleInsertCommand: ICommandFunc<AppUserRoleInsertCommandInputDTO, OperationResponse<AppUserRoleInsertCommandOutputDTO>>
    {
    }
}