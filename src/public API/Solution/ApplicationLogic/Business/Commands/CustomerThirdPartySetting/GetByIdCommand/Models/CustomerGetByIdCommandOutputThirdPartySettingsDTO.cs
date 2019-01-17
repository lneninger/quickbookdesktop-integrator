using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetByIdCommand.Models
{
    public class CustomerThirdPartyAppSettingGetByIdCommandOutputThirdPartySettingsDTO
    {
        public int Id { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyAppCustomerThirdPartyAppSettingId { get; set; }
    }
}
