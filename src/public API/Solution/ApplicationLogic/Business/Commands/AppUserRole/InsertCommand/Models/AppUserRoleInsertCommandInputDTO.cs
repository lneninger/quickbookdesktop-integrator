using System;
using System.Collections.Generic;

namespace ApplicationLogic.Business.Commands.AppUserRole.InsertCommand.Models
{
    public class AppUserRoleInsertCommandInputDTO: ICloneable
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
       

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}