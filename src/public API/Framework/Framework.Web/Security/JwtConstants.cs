using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security
{
    public class JwtConstants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id", Permissions = "per";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string Administrator = "administrator";
            }
        }
    }
}
