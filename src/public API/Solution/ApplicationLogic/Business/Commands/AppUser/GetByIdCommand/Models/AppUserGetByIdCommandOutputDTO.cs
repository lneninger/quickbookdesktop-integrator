using System;
using System.Collections.Generic;
using ApplicationLogic.Business.Commands.AppUserRole.GetByIdCommand.Models;

namespace ApplicationLogic.Business.Commands.AppUser.GetByIdCommand.Models
{
    public class AppUserGetByIdCommandOutputDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }

        public List<AppUserRoleGetByIdCommandOutputUserDTO> UserRoles { get; set; }
    }
}