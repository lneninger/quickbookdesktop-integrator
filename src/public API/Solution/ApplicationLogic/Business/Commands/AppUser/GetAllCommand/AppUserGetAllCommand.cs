using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUser.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.AppUser.GetAllCommand
{
    public class AppUserGetAllCommand : AbstractDBCommand<DomainModel.Identity.AppUser, IAppUserDBRepository>, IAppUserGetAllCommand
    {
        public AppUserGetAllCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IEnumerable<AppUserGetAllCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<AppUserGetAllCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new AppUserGetAllCommandOutputDTO
                    {
                        Id = entityItem.Id,
                        UserName = entityItem.UserName,
                        Email = entityItem.Email

                    }).ToList();
                }
            }

            return result;
        }
    }
}
