using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand
{
    public interface ICustomerThirdPartyAppSettingUpdateCommand: ICommandFunc<CustomerThirdPartyAppSettingUpdateCommandInputDTO, OperationResponse<CustomerThirdPartyAppSettingUpdateCommandOutputDTO>>
    {
    }
}