using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByNameCommand.Models
{
    public class AppUserRoleGetByNameCommandOutputDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}