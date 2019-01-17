using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetByIdCommand.Models;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetByIdCommand
{
    public interface ICustomerThirdPartyAppSettingGetByIdCommand: ICommandFunc<int, OperationResponse<CustomerThirdPartyAppSettingGetByIdCommandOutputDTO>>
    {
    }
}