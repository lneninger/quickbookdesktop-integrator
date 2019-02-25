using ApplicationLogic.Business.Commands.PriceLevel.InsertCommand.Models;
using ApplicationLogic.Business.Commons.Validators;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.PriceLevel.RegisterValidator
{
    public interface IPriceLevelInsertValidator: IValidator<PriceLevelInsertCommandInputDTO>
    {
    }
}