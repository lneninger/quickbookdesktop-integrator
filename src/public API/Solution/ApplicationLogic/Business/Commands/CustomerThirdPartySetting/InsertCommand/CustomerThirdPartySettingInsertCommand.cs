using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.InsertCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.InsertCommand
{
    public class CustomerThirdPartyAppSettingThirdPartySettingInsertCommand : AbstractDBCommand<DomainModel.CustomerThirdPartyAppSetting, ICustomerThirdPartyAppSettingDBRepository>, ICustomerThirdPartyAppSettingInsertCommand
    {
        public CustomerThirdPartyAppSettingThirdPartySettingInsertCommand(IDbContextScopeFactory dbContextScopeFactory, ICustomerThirdPartyAppSettingDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<CustomerThirdPartyAppSettingInsertCommandOutputDTO> Execute(CustomerThirdPartyAppSettingInsertCommandInputDTO input)
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.Insert(input);
            }
        }
    }
}
