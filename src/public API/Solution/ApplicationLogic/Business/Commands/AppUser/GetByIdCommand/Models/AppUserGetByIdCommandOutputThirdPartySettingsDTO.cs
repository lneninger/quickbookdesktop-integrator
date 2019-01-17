using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.AppUser.GetByIdCommand.Models
{
    public class AppUserGetByIdCommandOutputThirdPartySettingsDTO
    {
        public int Id { get; set; }
        public string ThirdPartyAppTypeId { get; set; }
        public string ThirdPartyAppUserId { get; set; }
    }
}
