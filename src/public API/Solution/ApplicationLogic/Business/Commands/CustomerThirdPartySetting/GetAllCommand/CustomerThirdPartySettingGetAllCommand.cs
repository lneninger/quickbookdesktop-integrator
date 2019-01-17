using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetAllCommand.Models;
using Framework.Core.Messages;
using System.Linq;

namespace ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetAllCommand
{
    public class CustomerThirdPartyAppSettingGetAllCommand : AbstractDBCommand<DomainModel.CustomerThirdPartyAppSetting, ICustomerThirdPartyAppSettingDBRepository>, ICustomerThirdPartyAppSettingGetAllCommand
    {
        public CustomerThirdPartyAppSettingGetAllCommand(IDbContextScopeFactory dbContextScopeFactory, ICustomerThirdPartyAppSettingDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IEnumerable<CustomerThirdPartyAppSettingGetAllCommandOutputDTO>> Execute()
        {
            var result = new OperationResponse<IEnumerable<CustomerThirdPartyAppSettingGetAllCommandOutputDTO>>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getAllResult = this.Repository.GetAll();
                result.AddResponse(getAllResult);
                if (result.IsSucceed)
                {
                    result.Bag = getAllResult.Bag.Select(entityItem => new CustomerThirdPartyAppSettingGetAllCommandOutputDTO
                    {
                        Id = entityItem.Id,
                        CustomerId = entityItem.CustomerId,
                        ThirdPartyAppTypeId = entityItem.ThirdPartyAppTypeId,
                        ThirdPartyCustomerId = entityItem.ThirdPartyCustomerId,
                        CreatedAt = entityItem.CreatedAt
                    }).ToList();
                }
            }

            return result;


            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                

                
            }
        }
    }
}
