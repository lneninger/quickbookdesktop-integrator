using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.DeleteCommand.Models
{
    public class CustomerThirdPartyAppSettingDeleteCommandOutputDTO
    {

        public CustomerThirdPartyAppSettingDeleteCommandOutputDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyCustomerId { get; set; }
    }
}