using ApplicationLogic.Business.Commands.InventoryAccount.InsertCommand.Models;
using ApplicationLogic.Business.Commons.Validators;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.InventoryAccount.RegisterValidator
{
    public interface IInventoryAccountInsertValidator: IValidator<InventoryAccountInsertCommandInputDTO>
    {
    }
}