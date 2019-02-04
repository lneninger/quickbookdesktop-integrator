using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;
using FluentValidation;

namespace ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand
{
    public class InventoryItemSyncValidator : FluentValidation.AbstractValidator<InventoryItemInsertCommandInputDTO>
    {
        public InventoryItemSyncValidator()
        {
            // Email Required
            this.RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
