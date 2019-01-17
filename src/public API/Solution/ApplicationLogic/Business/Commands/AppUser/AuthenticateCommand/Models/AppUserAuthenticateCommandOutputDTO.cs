using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUser.AuthenticateCommand.Models
{
    public class AppUserAuthenticateCommandOutputDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}