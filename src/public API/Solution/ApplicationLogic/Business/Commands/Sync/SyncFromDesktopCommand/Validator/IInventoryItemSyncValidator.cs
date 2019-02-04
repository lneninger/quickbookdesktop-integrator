using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models;
using ApplicationLogic.Business.Commons.Validators;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand
{
    public interface IInventoryItemInsertValidator: IValidator<InventoryItemInsertCommandInputDTO>
    {
    }
}