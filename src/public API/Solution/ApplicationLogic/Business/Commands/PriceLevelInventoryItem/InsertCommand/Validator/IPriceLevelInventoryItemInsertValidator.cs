using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand.Models;
using ApplicationLogic.Business.Commons.Validators;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.RegisterValidator
{
    public interface IPriceLevelInventoryItemInsertValidator: IValidator<PriceLevelInventoryItemInsertCommandInputDTO>
    {
    }
}