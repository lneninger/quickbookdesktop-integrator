using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByIdCommand.Models
{
    public class AppUserRoleGetByIdCommandOutputDTO
    {
        public AppUserRoleGetByIdCommandOutputDTO()
        {
            this.RoleUsers = new List<AppUserRoleGetByIdCommandOutputUserDTO>();
            this.RolePermissions = new List<AppUserRoleGetByIdCommandOutputPermissionDTO>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public List<AppUserRoleGetByIdCommandOutputPermissionDTO> RolePermissions { get; set; }

        public List<AppUserRoleGetByIdCommandOutputUserDTO> RoleUsers { get; set; }
    }
}