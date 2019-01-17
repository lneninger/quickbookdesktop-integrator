using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUser.DeleteCommand.Models
{
    public class AppUserDeleteCommandOutputDTO
    {

        public AppUserDeleteCommandOutputDTO()
        {
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}