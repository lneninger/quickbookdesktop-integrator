using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Security
{
    public class AppUserUpdatePasswordCommandInputDTO
    {
        public string UserId { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}