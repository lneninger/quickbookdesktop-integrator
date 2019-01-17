using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand.Models;
using Framework.Core.Messages;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand
{
    public class CustomerThirdPartyAppSettingUpdateCommand : AbstractDBCommand<DomainModel.CustomerThirdPartyAppSetting, ICustomerThirdPartyAppSettingDBRepository>, ICustomerThirdPartyAppSettingUpdateCommand
    {
        public CustomerThirdPartyAppSettingUpdateCommand(IDbContextScopeFactory dbContextScopeFactory, ICustomerThirdPartyAppSettingDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<CustomerThirdPartyAppSettingUpdateCommandOutputDTO> Execute(CustomerThirdPartyAppSettingUpdateCommandInputDTO input)
        {
            var result = new OperationResponse<CustomerThirdPartyAppSettingUpdateCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(input.Id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    getByIdResult.Bag.CustomerId = input.CustomerId;
                    getByIdResult.Bag.ThirdPartyAppTypeId = input.ThirdPartyAppTypeId;
                    getByIdResult.Bag.ThirdPartyCustomerId = input.ThirdPartyCustomerId;

                    try
                    {
                        dbContextScope.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.AddError("Error updating Product Color Type", ex);
                    }

                    getByIdResult = this.Repository.GetById(input.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new CustomerThirdPartyAppSettingUpdateCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            CustomerId = getByIdResult.Bag.CustomerId,
                            CustomerName = getByIdResult.Bag.Customer.Name,
                            ThirdPartyAppTypeId = getByIdResult.Bag.ThirdPartyAppTypeId,
                            ThirdPartyCustomerId = getByIdResult.Bag.ThirdPartyCustomerId,
                        };
                    }

                }
            }

            return result;
        }
    }
}
