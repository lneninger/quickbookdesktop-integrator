using ApplicationLogic.Business.Commons.DTOs;
using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commons
{
    public class MasterDataProvider: IMasterDataProvider
    {
        public IMasterDBRepository Repository { get; }
        public IDbContextScopeFactory DbContextScopeFactory { get; }

        public MasterDataProvider(IDbContextScopeFactory dbContextScopeFactory, IMasterDBRepository repository)
        {
            this.DbContextScopeFactory = dbContextScopeFactory;
            this.Repository = repository;
        }


        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumThirdPartyAppTypes()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumThirdPartyAppType();
            }

        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumProductColorTypes()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumProductColorType();
            }

        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumCustomerFreightoutRateTypes()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumCustomerFreightoutRateType();
            }
        }


        public OperationResponse<List<EnumItemDTO<int>>> GetToEnumCustomers()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumCustomer();
            }
        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumAppUsers()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumUser();
            }
        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumProductTypes()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumProductType();
            }
        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumRoles()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumRole();
            }
        }

        public OperationResponse<List<EnumItemDTO<int>>> GetToEnumProducts()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumProducts();
            }
        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumSeasonCategories()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumSeasonCategories();
            }
        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumSeasonCategoriesWithSeasons()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumSeasonCategoriesWithSeasons();
            }
        }

        public OperationResponse<List<EnumItemDTO<string>>> GetToEnumGrowerTypesWithGrower()
        {
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                return this.Repository.GetToEnumGrowerTypesWithGrower();
            }
        }
    }
}
