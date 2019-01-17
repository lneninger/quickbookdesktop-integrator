using ApplicationLogic.Business.Commands.AppUser.AuthenticateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUser.AuthenticateCommand
{
    public interface IAppUserAuthenticateCommand : ICommandFunc<AppUserAuthenticateCommandInputDTO, OperationResponse<AppUserAuthenticateCommandOutputDTO>>
    {
    }
}