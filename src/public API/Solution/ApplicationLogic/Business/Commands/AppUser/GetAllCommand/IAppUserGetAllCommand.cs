using ApplicationLogic.Business.Commands.AppUser.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUser.GetAllCommand
{
    public interface IAppUserGetAllCommand: ICommandAction<OperationResponse<IEnumerable<AppUserGetAllCommandOutputDTO>>>
    {
    }
}