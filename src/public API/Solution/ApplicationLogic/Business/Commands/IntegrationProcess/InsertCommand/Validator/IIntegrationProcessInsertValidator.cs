using ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand.Models;
using ApplicationLogic.Business.Commons.Validators;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IntegrationProcess.RegisterValidator
{
    public interface IIntegrationProcessInsertValidator: IValidator<IntegrationProcessInsertCommandInputDTO>
    {
    }
}