using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commands.Security
{
    public class AppUserClaimInsertCommandInputDTO
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string permissionId { get; set; }
    }
}
