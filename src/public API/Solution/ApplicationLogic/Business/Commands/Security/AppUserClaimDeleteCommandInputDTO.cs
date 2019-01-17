using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.Security
{
    public class AppUserClaimDeleteCommandInputDTO
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string Claim { get; set; }
    }
}
