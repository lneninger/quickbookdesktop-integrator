using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUser.RegisterCommand.Models
{
    public class AppUserRegisterCommandInputDTO: ICloneable
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string NormalizedEmail { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}