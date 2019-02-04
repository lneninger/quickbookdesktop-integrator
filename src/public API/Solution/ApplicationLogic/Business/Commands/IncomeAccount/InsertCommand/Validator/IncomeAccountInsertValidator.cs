using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;
using FluentValidation;

namespace ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand
{
    public class IncomeAccountInsertValidator : FluentValidation.AbstractValidator<IncomeAccountInsertCommandInputDTO>
    {
        public IncomeAccountInsertValidator()
        {
            // Email Required
            this.RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
