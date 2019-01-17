using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUser.RegisterCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;
using FluentValidation;

namespace ApplicationLogic.Business.Commands.AppUser.RegisterCommand
{
    public class AppUserRegisterValidator : FluentValidation.AbstractValidator<AppUserRegisterCommandInputDTO>, IAppUserRegisterValidator
    {
        public AppUserRegisterValidator(IAppUserDBRepository repository)
        {
            this.Repository = repository;

            // Email Required
            this.RuleFor(x => x.NormalizedEmail)
                .NotEmpty();

            // Email has email address
            this.RuleFor(x => x.NormalizedEmail)
                .EmailAddress();

            // User Name Required
            this.RuleFor(x => x.UserName)
                .NotEmpty();

            // User Name minimum length
            this.RuleFor(x => x.UserName)
                .MinimumLength(3);

            //Email doesn't exists
            this.When(x => !string.IsNullOrWhiteSpace(x.NormalizedEmail), () =>
            {
                this.RuleFor(x => x.NormalizedEmail)
                .Must(email =>
                {
                    var result = this.Repository.ExistsByEmail(email);
                    return result.IsSucceed && result.Bag;
                });
            });

            //UserName doesn't exists
            this.When(x => !string.IsNullOrWhiteSpace(x.UserName), () =>
            {
                this.RuleFor(x => x.UserName)
                .Must(userName =>
                {
                    var result = this.Repository.ExistsByUserName(userName);
                    return result.IsSucceed && result.Bag;
                });
            });
        }

        public IAppUserDBRepository Repository { get; }
    }
}
