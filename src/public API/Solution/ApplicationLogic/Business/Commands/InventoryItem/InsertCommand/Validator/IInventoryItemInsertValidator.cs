using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models;
using ApplicationLogic.Business.Commons.Validators;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryItem.RegisterValidator
{
    public interface IInventoryItemInsertValidator: IValidator<InventoryItemInsertCommandInputDTO>
    {
    }
}