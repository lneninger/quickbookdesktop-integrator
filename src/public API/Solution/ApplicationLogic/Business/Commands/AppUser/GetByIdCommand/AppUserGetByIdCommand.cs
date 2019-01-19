using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand.Models;
using Framework.Core.Messages;
using System.Linq;
using ApplicationLogic.Business.Commands.AppUserRole.GetByIdCommand.Models;

namespace ApplicationLogic.Business.Commands.AppUser.GetByIdCommand
{
    public class AppUserGetByIdCommand : AbstractDBCommand<DomainModel.Identity.AppUser, IAppUserDBRepository>, IAppUserGetByIdCommand
    {

        public AppUserGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserGetByIdCommandOutputDTO> Execute(string id)
        {
            var result = new OperationResponse<AppUserGetByIdCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    result.Bag = new AppUserGetByIdCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        Email = getByIdResult.Bag.Email,
                        UserName = getByIdResult.Bag.UserName,
                        FirstName = getByIdResult.Bag.FirstName,
                        LastName = getByIdResult.Bag.LastName,
                        PictureUrl = getByIdResult.Bag.PictureUrl,
                    };

                    var getRolesByIdResult = this.Repository.GetRolesByUserId(id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag.UserRoles = getRolesByIdResult.Bag.Select(userEntityItem => new AppUserRoleGetByIdCommandOutputUserDTO
                        {
                            Id = userEntityItem.Id,
                            UserId = result.Bag.Id,
                            RoleId = userEntityItem.Id
                        }).ToList();
                    }
                }
            }

            return result;
        }
    }
}
