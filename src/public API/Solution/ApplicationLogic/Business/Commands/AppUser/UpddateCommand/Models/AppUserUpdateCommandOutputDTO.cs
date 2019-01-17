using System;

namespace ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models
{
    public class AppUserUpdateCommandOutputDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}