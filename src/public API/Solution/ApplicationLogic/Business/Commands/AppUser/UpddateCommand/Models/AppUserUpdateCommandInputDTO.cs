using System;

namespace ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models
{
    public class AppUserUpdateCommandInputDTO
    {

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PictureUrl { get; set; }
    }
}