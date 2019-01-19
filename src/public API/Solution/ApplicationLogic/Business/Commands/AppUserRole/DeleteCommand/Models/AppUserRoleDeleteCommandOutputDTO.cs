using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.DeleteCommand.Models
{
    public class AppUserRoleDeleteCommandOutputDTO
    {

        public AppUserRoleDeleteCommandOutputDTO()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}