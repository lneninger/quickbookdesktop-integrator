using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByNameCommand.Models
{
    public class AppUserRoleGetByNameCommandOutputThirdPartySettingsDTO
    {
        public int Id { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyAppUserRoleId { get; set; }
    }
}
