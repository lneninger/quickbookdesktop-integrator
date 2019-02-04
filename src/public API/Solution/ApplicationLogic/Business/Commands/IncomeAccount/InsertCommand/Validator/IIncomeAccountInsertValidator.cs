using ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand.Models;
using ApplicationLogic.Business.Commons.Validators;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IncomeAccount.RegisterValidator
{
    public interface IIncomeAccountInsertValidator: IValidator<IncomeAccountInsertCommandInputDTO>
    {
    }
}