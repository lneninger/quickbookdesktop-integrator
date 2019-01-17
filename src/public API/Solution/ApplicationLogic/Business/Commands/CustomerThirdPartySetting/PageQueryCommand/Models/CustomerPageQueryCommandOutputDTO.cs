using System;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.PageQueryCommand.Models
{
    public class CustomerThirdPartyAppSettingPageQueryCommandOutputDTO
    {

        public int Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CustomerName { get; set; }

        public int CustomerId { get; set; }

        public string ThirdPartyAppTypeId { get; set; }

        public string ThirdPartyCustomerId { get; set; }
    }
}