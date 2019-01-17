using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.InsertCommand.Models
{
    public class CustomerThirdPartyAppSettingInsertCommandInputDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyCustomerId { get; set; }
    }
}