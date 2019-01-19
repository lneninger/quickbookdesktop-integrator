using System;

namespace ApplicationLogic.Business.Commands.AppUserRole.UpdateCommand.Models
{
    public class AppUserRoleUpdateCommandOutputDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}