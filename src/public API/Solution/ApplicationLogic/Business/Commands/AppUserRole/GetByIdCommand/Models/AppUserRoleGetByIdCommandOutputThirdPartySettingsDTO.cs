using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByIdCommand.Models
{
    public class AppUserRoleGetByIdCommandOutputThirdPartySettingsDTO
    {
        public int Id { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyAppUserRoleId { get; set; }
    }
}
