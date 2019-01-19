using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUserRole.GetByIdCommand.Models;
using Microsoft.AspNetCore.Identity;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.AppUserRole.GetByIdCommand
{
    public class AppUserRoleGetByIdCommand : AbstractDBCommand<IdentityRole, IAppUserRoleDBRepository>, IAppUserRoleGetByIdCommand
    {

        public AppUserRoleGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserRoleDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserRoleGetByIdCommandOutputDTO> Execute(string id)
        {
            var result = new OperationResponse<AppUserRoleGetByIdCommandOutputDTO> { };

            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    var entityItem = getByIdResult.Bag;
                    result.Bag = new AppUserRoleGetByIdCommandOutputDTO
                    {
                        Id = entityItem.Id,
                        Name = entityItem.Name,
                        NormalizedName = entityItem.NormalizedName,
                    };

                    var getUsersByIdResult = this.Repository.GetUsersByRoleId(id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag.RoleUsers = getUsersByIdResult.Bag.Select(userEntityItem => new AppUserRoleGetByIdCommandOutputUserDTO
                        {
                            Id = userEntityItem.Id,
                            RoleId = result.Bag.Id,
                            UserId = userEntityItem.Id
                        }).ToList();

                        var getPermissionsByIdResult = this.Repository.GetPermissionsByRoleId(id);
                        result.AddResponse(getByIdResult);
                        if (result.IsSucceed)
                        {
                            result.Bag.RolePermissions = getPermissionsByIdResult.Bag.Select(roleClaim => new AppUserRoleGetByIdCommandOutputPermissionDTO
                            {
                                RoleId = roleClaim.RoleId,
                                PermissionId = roleClaim.ClaimValue,
                            }).ToList();
                        }
                    }
                }
            }

            return result;
        }
    }
}
