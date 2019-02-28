using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;
using FluentValidation;

namespace ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand
{
    public class PriceLevelInventoryItemInsertValidator : FluentValidation.AbstractValidator<PriceLevelInventoryItemInsertCommandInputDTO>
    {
        public PriceLevelInventoryItemInsertValidator()
        {
            // Email Required
            this.RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
