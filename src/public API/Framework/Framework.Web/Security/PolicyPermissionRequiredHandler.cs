using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Security
{
    public class PolicyPermissionRequiredHandler : IAuthorizationHandler// AuthorizationHandler<PolicyPermissionRequired>

    {
        public Task HandleAsync(AuthorizationHandlerContext context/*, PolicyPermissionRequired requirement*/)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            bool allowed = false;

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is PolicyPermissionRequired)
                {
                    allowed = VerifyPermissionRequired(context, requirement as PolicyPermissionRequired);
                    if (allowed)
                    {
                        break;
                    }
                }
                else if (requirement is RolesAuthorizationRequirement)
                {
                    allowed = VerifyRolePermission(context, requirement as RolesAuthorizationRequirement);
                    if (allowed)
                    {
                        break;
                    }
                }
            }

            if (allowed) {
                pendingRequirements.ForEach(requirement =>
                {
                    context.Succeed(requirement);
                });
            }
            
            return Task.CompletedTask;
        }

        private static bool VerifyPermissionRequired(AuthorizationHandlerContext context, PolicyPermissionRequired requirement)
        {
            var permissions = context.User.FindAll(JwtConstants.Strings.JwtClaimIdentifiers.Permissions);//  FindFirst(c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == "http://contoso.com").Value);

            var matchClaimCount = permissions.Count(permission => permission.Value == requirement.Permission);

            return (matchClaimCount > 0);
        }

        private static bool VerifyRolePermission(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            var roleClaims = context.User.FindAll(JwtConstants.Strings.JwtClaimIdentifiers.Rol);//  FindFirst(c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == "http://contoso.com").Value);

            var matchClaimCount = roleClaims.Count(roleClaim => requirement.AllowedRoles.Contains(roleClaim.Value));

            return (matchClaimCount > 0);
        }
    }
}
