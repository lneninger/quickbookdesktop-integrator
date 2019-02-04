using ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand
{
    public interface ISyncFromDesktopCommand: ICommandFunc<SyncFromDesktopCommandInputDTO, OperationResponse>
    {
    }
}