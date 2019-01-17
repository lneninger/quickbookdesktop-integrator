using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetByIdCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetByIdCommand
{
    public class CustomerThirdPartyAppSettingGetByIdCommand : AbstractDBCommand<DomainModel.CustomerThirdPartyAppSetting, ICustomerThirdPartyAppSettingDBRepository>, ICustomerThirdPartyAppSettingGetByIdCommand
    {

        public CustomerThirdPartyAppSettingGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, ICustomerThirdPartyAppSettingDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<CustomerThirdPartyAppSettingGetByIdCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<CustomerThirdPartyAppSettingGetByIdCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    result.Bag = new CustomerThirdPartyAppSettingGetByIdCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        CustomerName = getByIdResult.Bag.Customer.Name,
                        ThirdPartyAppTypeId = getByIdResult.Bag.ThirdPartyAppTypeId,
                        ThirdPartyCustomerId = getByIdResult.Bag.ThirdPartyCustomerId,
                    };
                }
            }

            return result;
        }
    }
}
