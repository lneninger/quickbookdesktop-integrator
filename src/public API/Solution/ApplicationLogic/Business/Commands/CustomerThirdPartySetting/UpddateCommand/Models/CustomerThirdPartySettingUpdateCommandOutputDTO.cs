using System;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand.Models
{
    public class CustomerThirdPartyAppSettingUpdateCommandOutputDTO
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public int CustomerId { get; set; }

        public string ThirdPartyAppTypeId { get; set; }

        public string ThirdPartyCustomerId { get; set; }
    }
}