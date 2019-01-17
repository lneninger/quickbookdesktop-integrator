using ApplicationLogic.Business.Commons.DTOs;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commons
{
    public interface IMasterDataProvider
    {
        OperationResponse<List<EnumItemDTO<string>>> GetToEnumThirdPartyAppTypes();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumProductColorTypes();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumCustomerFreightoutRateTypes();

        OperationResponse<List<EnumItemDTO<int>>> GetToEnumCustomers();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumAppUsers();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumProductTypes();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumRoles();

        OperationResponse<List<EnumItemDTO<int>>> GetToEnumProducts();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumSeasonCategories();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumSeasonCategoriesWithSeasons();

        OperationResponse<List<EnumItemDTO<string>>> GetToEnumGrowerTypesWithGrower();
    }
}
