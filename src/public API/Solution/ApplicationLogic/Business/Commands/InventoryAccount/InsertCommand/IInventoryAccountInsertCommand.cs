using ApplicationLogic.Business.Commands.InventoryAccount.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.InsertCommand
{
    public interface IInventoryAccountInsertCommand: ICommandFunc<InventoryAccountInsertCommandInputDTO, OperationResponse<InventoryAccountInsertCommandOutputDTO>>
    {
    }
}