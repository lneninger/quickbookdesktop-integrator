using ApplicationLogic.Business.Commands.Product.DeleteCommand.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Product.DeleteCommand
{
    public interface IFunzaIntegrationAuthenticateCommand: ICommandFunc<int, OperationResponse<ProductDeleteCommandOutputDTO>>
    {
    }
}