using ApplicationLogic.Business.Commands.AppUser.RegisterCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUser.RegisterCommand
{
    public interface IAppUserRegisterCommand: ICommandFunc<AppUserRegisterCommandInputDTO, OperationResponse<AppUserRegisterCommandOutputDTO>>
    {
    }
}