using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.AppUser.GetByIdCommand
{
    public interface IAppUserGetByIdCommand: ICommandFunc<string, OperationResponse<AppUserGetByIdCommandOutputDTO>>
    {
    }
}