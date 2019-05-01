using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;
using FluentValidation;

namespace ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand
{
    public class IntegrationProcessInsertValidator : FluentValidation.AbstractValidator<IntegrationProcessInsertCommandInputDTO>
    {
        public IntegrationProcessInsertValidator()
        {
            
        }
    }
}
