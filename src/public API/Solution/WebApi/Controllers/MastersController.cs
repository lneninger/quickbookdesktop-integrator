using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunicationModel;
using FizzWare.NBuilder;
using ApplicationLogic.Business.Commands.Customer.GetAllCommand;
using ApplicationLogic.Business.Commands.Customer.GetByIdCommand;
using ApplicationLogic.Business.Commands.Customer.InsertCommand;
using ApplicationLogic.Business.Commands.Customer.InsertCommand.Models;
using ApplicationLogic.Business.Commands.Customer.UpdateCommand;
using ApplicationLogic.Business.Commands.Customer.UpdateCommand.Models;
using ApplicationLogic.Business.Commands.Customer.DeleteCommand;
using ApplicationLogic.Business.Commands.Customer.DeleteCommand.Models;
using ApplicationLogic.Business.Interfaces;
//using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationLogic.Business.Commands.Customer.PageQueryCommand;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using ApplicationLogic.Business.Commands.Customer.PageQueryCommand.Models;
using ApplicationLogic.Business.Commons;
using ApplicationLogic.Business.Commons.DTOs;
using RiverdaleMainApp2_0.Auth;
using Authorization = Microsoft.AspNetCore.Authorization;
using ApplicationLogic.SignalR;
//using Microsoft.AspNet.SignalR;
using Framework.Core.Messages;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// Customer API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/masters")]
    public class MastersController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersController"/> class.
        /// </summary>
        /// <param name="masterDataProvider"></param>
        public MastersController(/*IHubContext<GlobalHub> hubContext, */IMasterDataProvider masterDataProvider) : base(/*hubContext*/)
        {
            this.MasterDataProvider = masterDataProvider;
        }

        /// <summary>
        /// Gets the customer service.
        /// </summary>
        /// <value>
        /// The customer service.
        /// </value>
        public IMasterDataProvider MasterDataProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("thirdpartyapptype")]
        public IActionResult ThirdPartyAppType()
        {
            var result = this.MasterDataProvider.GetToEnumThirdPartyAppTypes();

            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("productcolortype")]
        public IActionResult ProductColorTypes()
        {
            var result = this.MasterDataProvider.GetToEnumProductColorTypes();
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("customerfreightoutratetype")]
        public IActionResult CustomerFreightoutRateTypes()
        {
            var result = this.MasterDataProvider.GetToEnumCustomerFreightoutRateTypes();
            return this.Ok(result);
        }

        /// <summary>
        /// Customers this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("customer")]
        public IActionResult Customers()
        {
            var result = this.MasterDataProvider.GetToEnumCustomers();
            return this.Ok(result);
        }

        /// <summary>
        /// Users this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("user")]
        public IActionResult GetToEnumAppUsers()
        {
            var result = this.MasterDataProvider.GetToEnumAppUsers();
            return this.Ok(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("permission")]
        public IActionResult GetToEnumPermissions()
        {
            var @enumNames = Enum.GetNames(typeof(PermissionsEnum.Enum));
            var result = @enumNames.Select(enumName => new EnumItemDTO<string> { Key = enumName, Value = enumName });
            return this.Ok(new OperationResponse<IEnumerable<EnumItemDTO<string>>>(result, null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("role")]
        public IActionResult GetToEnumRoles()
        {
            var result = this.MasterDataProvider.GetToEnumRoles();
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("producttype")]
        public IActionResult GetToEnumProductTypes()
        {
            var result = this.MasterDataProvider.GetToEnumProductTypes();
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<int>>))]
        [Route("product")]
        public IActionResult GetToEnumProducts()
        {
            var result = this.MasterDataProvider.GetToEnumProducts();
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("seasonCategory")]
        public IActionResult GetToEnumSeasonCategories()
        {
            var result = this.MasterDataProvider.GetToEnumSeasonCategories();
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("seasonCategoryWithSeasons")]
        public IActionResult GetToEnumSeasonCategoriesWithSeasons()
        {
            var result = this.MasterDataProvider.GetToEnumSeasonCategoriesWithSeasons();
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, ProducesResponseType(200, Type = typeof(List<EnumItemDTO<string>>))]
        [Route("growerTypeWithGrowers")]
        public IActionResult GetToEnumGrowerTypeWithGrowers()
        {
            var result = this.MasterDataProvider.GetToEnumGrowerTypesWithGrower();
            return this.Ok(result);
        }
    }
}
