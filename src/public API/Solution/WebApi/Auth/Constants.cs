using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverdaleMainApp2_0.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// 
        /// </summary>
        public static class Strings
        {
            /// <summary>
            /// 
            /// </summary>
            public static class JwtClaimIdentifiers
            {
                /// <summary>
                /// 
                /// </summary>
                public const string Rol = "rol", Id = "id", Permissions = "per";
            }

            /// <summary>
            /// 
            /// </summary>
            public static class JwtClaims
            {
                /// <summary>
                /// 
                /// </summary>
                public const string ApiAccess = "api_access";

                /// <summary>
                /// 
                /// </summary>
                public const string Administrator = "administrator";
            }
        }
    }
}
