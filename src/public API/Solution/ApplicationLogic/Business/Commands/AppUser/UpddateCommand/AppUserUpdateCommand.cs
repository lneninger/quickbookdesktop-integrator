using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.AppUser.UpdateCommand
{
    public class AppUserUpdateCommand : AbstractDBCommand<DomainModel.Identity.AppUser, IAppUserDBRepository>, IAppUserUpdateCommand
    {
        public AppUserUpdateCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<AppUserUpdateCommandOutputDTO> Execute(AppUserUpdateCommandInputDTO input)
        {
            var result = new OperationResponse<AppUserUpdateCommandOutputDTO>();
            try
            {
                using (var dbContextScope = this.DbContextScopeFactory.Create())
                {
                    var getByIdResult = this.Repository.GetById(input.Id);
                    result.AddResponse(getByIdResult);
                    if (getByIdResult.IsSucceed)
                    {
                        var entity = getByIdResult.Bag;
                        entity.FirstName = input.FirstName;
                        entity.LastName = input.LastName;
                        entity.PictureUrl = input.PictureUrl;
                        try
                        {
                            dbContextScope.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            result.AddException($"Error updating user", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Fatal error updating user", ex);
            }

            return result;
        }
    }
}
