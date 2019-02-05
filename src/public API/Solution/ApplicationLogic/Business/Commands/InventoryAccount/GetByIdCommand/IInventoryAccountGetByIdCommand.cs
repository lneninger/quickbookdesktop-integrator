using ApplicationLogic.Business.Commands.InventoryAccount.GetByIdCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.InventoryAccount.GetByIdCommand
{
    public interface IInventoryAccountGetByIdCommand: ICommandFunc<int, OperationResponse<InventoryAccountGetByIdCommandOutputDTO>>
    {
    }
}