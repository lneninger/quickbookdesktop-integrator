using ApplicationLogic.Business.Commands.InventoryAccount.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.UpdateCommand
{
    public interface IInventoryAccountUpdateCommand: ICommandFunc<InventoryAccountUpdateCommandInputDTO, OperationResponse<InventoryAccountUpdateCommandOutputDTO>>
    {
    }
}