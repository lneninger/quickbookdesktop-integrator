using ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand
{
    public interface IIntegrationProcessInsertCommand: ICommandFunc<IntegrationProcessInsertCommandInputDTO, OperationResponse<IntegrationProcessInsertCommandOutputDTO>>
    {
    }
}