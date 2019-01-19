using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand.Models;
using ApplicationLogic.Repositories.DB;
using Framework.Autofac;
using Framework.EF.DbContextImpl;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;

namespace ApplicationLogic.SignalR
{
    public class BaseSignalR<T> : Framework.SignalR.BaseHub<T> where T: class
    {
        public BaseSignalR(ICurrentUserService currentUserService, IDbContextScopeFactory dbContextScopeFactory, IAppUserDBRepository repository)
        {
            this.CurrentUserService = currentUserService;
            this.DbContextScopeFactory = dbContextScopeFactory;
            this.Repository = repository;
        }

        public ICurrentUserService CurrentUserService { get; }
        public IDbContextScopeFactory DbContextScopeFactory { get; }
        public IAppUserDBRepository Repository { get; }

        protected override string GetUserName()
        {
            var currentUser = this.GetCurrentUser();
            if (currentUser != null && currentUser.IsSucceed)
            {
                return currentUser.Bag.Id;
            }

            return null;
        }

        protected OperationResponse<AppUserGetByIdCommandOutputDTO> GetCurrentUser()
        {
            var result = new OperationResponse<AppUserGetByIdCommandOutputDTO>();
            using (var scope = IoCGlobal.NewScope())
            {
                    using (var dbContextScope = this.DbContextScopeFactory.Create())
                    {
                        var getByIdResult = this.Repository.GetById(this.CurrentUserService.CurrentUserId);
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
                        }
                    }
            }

            return result;
        }
    }
}
