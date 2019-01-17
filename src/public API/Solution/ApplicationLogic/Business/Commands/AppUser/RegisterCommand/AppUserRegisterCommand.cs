using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.AppUser.RegisterCommand.Models;
using Framework.Core.Crypto;
using Framework.Core.Messages;
using Framework.FluentValidation;
using Framework.Core.GeneralValidations;

namespace ApplicationLogic.Business.Commands.AppUser.RegisterCommand
{
    public class AppUserRegisterCommand : AbstractDBCommand<DomainModel.Identity.AppUser, IAppUserDBRepository>, IAppUserRegisterCommand
    {
        public AppUserRegisterCommand(IDbContextScopeFactory dbContextScopeFactory, IAppUserRegisterValidator validator, IAppUserDBRepository repository) : base(dbContextScopeFactory, repository, validator)
        {
        }

        public OperationResponse<AppUserRegisterCommandOutputDTO> Execute(AppUserRegisterCommandInputDTO input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                this.FormatData(input);

                var result = new OperationResponse<AppUserRegisterCommandOutputDTO>().AddValidationResult(this.Validator.Validate(input));
                if (!result.IsSucceed) return result;

                var internalInput = input.Clone() as AppUserRegisterCommandInputDTO;
                byte[] passwordHash, passwordSalt;
                HashHelper.CreatePasswordHash(internalInput.Password, out passwordHash, out passwordSalt);

                internalInput.PasswordHash = passwordHash;
                internalInput.PasswordSalt = passwordSalt;

                return this.Repository.Insert(internalInput);
            }
        }

        private void FormatData(AppUserRegisterCommandInputDTO input)
        {
            input.NormalizedEmail = InternetFieldNormalization.NormalizeEmail(input.Email);
        }
    }
}
