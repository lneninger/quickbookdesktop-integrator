using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetAllCommand
{
    public interface ICustomerThirdPartyAppSettingGetAllCommand: ICommandAction<OperationResponse<IEnumerable<CustomerThirdPartyAppSettingGetAllCommandOutputDTO>>>
    {
    }
}