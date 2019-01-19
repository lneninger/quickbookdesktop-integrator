using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Identity
{
    public class AppUserRole: IdentityRole
    {
        public string Description { get; set; }
    }
}
