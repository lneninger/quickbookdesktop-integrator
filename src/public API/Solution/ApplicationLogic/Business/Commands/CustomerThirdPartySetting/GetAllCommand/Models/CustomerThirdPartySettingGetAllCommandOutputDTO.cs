using System;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetAllCommand.Models
{
    public class CustomerThirdPartyAppSettingGetAllCommandOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ERPId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyCustomerId { get; set; }
    }
}