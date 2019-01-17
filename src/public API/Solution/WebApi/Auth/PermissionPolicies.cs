using Framework.Web.Security;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverdaleMainApp2_0.Auth
{
    public class PermissionPolicies
    {
        public static Dictionary<string, Action<AuthorizationPolicyBuilder>> Policies { get; } = new Dictionary<string, Action<AuthorizationPolicyBuilder>>();

        static PermissionPolicies() {
            var permissionNames = Enum.GetNames(typeof(PermissionsEnum.Enum));

            foreach (var permissionName in permissionNames)
            {
                Policies.Add(permissionName, policy => policy.Requirements.Add(new PolicyPermissionRequired(PermissionsEnum.Customer_Read)));
            }
        }
    }
}
