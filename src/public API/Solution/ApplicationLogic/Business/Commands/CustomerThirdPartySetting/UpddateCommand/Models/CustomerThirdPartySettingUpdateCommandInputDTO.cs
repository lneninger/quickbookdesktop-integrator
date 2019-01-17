using System;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand.Models
{
    public class CustomerThirdPartyAppSettingUpdateCommandInputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
        public int CustomerId { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyCustomerId { get; set; }
    }
}