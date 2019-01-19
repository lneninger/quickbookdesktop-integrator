using ApplicationLogic.Business.Commands.AppUserRole.GetByNameCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByNameCommand
{
    public interface IAppUserRoleGetByNameCommand: ICommandFunc<string, OperationResponse<AppUserRoleGetByNameCommandOutputDTO>>
    {
    }
}