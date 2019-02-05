using ApplicationLogic.Business.Commands.InventoryAccount.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.GetAllCommand
{
    public interface IInventoryAccountGetAllCommand: ICommandAction<OperationResponse<IEnumerable<InventoryAccountGetAllCommandOutputDTO>>>
    {
    }
}