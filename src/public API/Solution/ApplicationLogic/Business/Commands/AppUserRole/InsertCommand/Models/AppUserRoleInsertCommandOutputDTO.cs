using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.InsertCommand.Models
{
    public class AppUserRoleInsertCommandOutputDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}