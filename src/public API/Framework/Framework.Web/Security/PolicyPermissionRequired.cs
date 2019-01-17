using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Security
{
    public class PolicyPermissionRequired : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PolicyPermissionRequired(string permission)
        {
            this.Permission = permission;
        }

        public static void BuildPolicies(AuthorizationOptions options, IEnumerable<string> permissionNames)
        {
            foreach (var permissionName in permissionNames)
            {
                options.AddPolicy(permissionName, policy => policy.Requirements.Add(new PolicyPermissionRequired(permissionName)));
            }
        }
    }
}
