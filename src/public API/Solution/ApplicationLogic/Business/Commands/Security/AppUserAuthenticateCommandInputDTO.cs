using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.Security
{
    public class AppUserAuthenticateCommandInputDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}