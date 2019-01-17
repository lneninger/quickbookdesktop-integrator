using ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUser.UpdateCommand
{
    public interface IAppUserUpdateCommand: ICommandFunc<AppUserUpdateCommandInputDTO, OperationResponse<AppUserUpdateCommandOutputDTO>>
    {
    }
}