using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunicationModel;
using FizzWare.NBuilder;
using ApplicationLogic.Business.Commands.ProductColorType.GetAllCommand;
using ApplicationLogic.Business.Commands.ProductColorType.GetByIdCommand;
using ApplicationLogic.Business.Commands.ProductColorType.InsertCommand;
using ApplicationLogic.Business.Commands.ProductColorType.InsertCommand.Models;
using ApplicationLogic.Business.Commands.ProductColorType.UpdateCommand;
using ApplicationLogic.Business.Commands.ProductColorType.UpdateCommand.Models;
using ApplicationLogic.Business.Commands.ProductColorType.DeleteCommand;
using ApplicationLogic.Business.Commands.ProductColorType.DeleteCommand.Models;
using ApplicationLogic.Business.Interfaces;
//using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationLogic.Business.Commands.ProductColorType.PageQueryCommand;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using ApplicationLogic.Business.Commands.ProductColorType.PageQueryCommand.Models;
using ApplicationLogic.Business.Commands.ProductColorType.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.ProductColorType.GetAllCommand.Models;
using Authorization = Microsoft.AspNetCore.Authorization;
using ApplicationLogic.SignalR;
//using Microsoft.AspNet.SignalR;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// ProductColorType API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/productcolortype")]
    public class ProductColorTypeController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductColorTypeController"/> class.
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="insertCommand">The insert command.</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public ProductColorTypeController(/*IHubContext<GlobalHub> hubContext, */IProductColorTypePageQueryCommand pageQueryCommand, IProductColorTypeGetAllCommand getAllCommand, IProductColorTypeGetByIdCommand getByIdCommand, IProductColorTypeInsertCommand insertCommand, IProductColorTypeUpdateCommand updateCommand, IProductColorTypeDeleteCommand deleteCommand):base(/*hubContext*/)
        {
            this.PageQueryCommand = pageQueryCommand;
            this.GetAllCommand = getAllCommand;
            this.GetByIdCommand = getByIdCommand;
            this.InsertCommand = insertCommand;
            this.UpdateCommand = updateCommand;
            this.DeleteCommand = deleteCommand;
        }

        /// <summary>
        /// Gets the get all command.
        /// </summary>
        /// <value>
        /// The get all command.
        /// </value>
        public IProductColorTypeGetAllCommand GetAllCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public IProductColorTypePageQueryCommand PageQueryCommand { get; }


        /// <summary>
        /// Gets the get by identifier command.
        /// </summary>
        /// <value>
        /// The get by identifier command.
        /// </value>
        public IProductColorTypeGetByIdCommand GetByIdCommand { get; }

        /// <summary>
        /// Gets the insert command.
        /// </summary>
        /// <value>
        /// The insert command.
        /// </value>
        public IProductColorTypeInsertCommand InsertCommand { get; }

        /// <summary>
        /// Gets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public IProductColorTypeUpdateCommand UpdateCommand { get; }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public IProductColorTypeDeleteCommand DeleteCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(PageResult<ProductColorTypePageQueryCommandOutputDTO>))]
        [Route("pagequery")]
        public IActionResult PageQuery([FromBody]PageQuery<ProductColorTypePageQueryCommandInputDTO> input)
        {
            var result = this.PageQueryCommand.Execute(input);

            return this.Ok(result);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name="GetAllProductColorType"), ProducesResponseType(200, Type = typeof(IEnumerable<ProductColorTypeGetAllCommandOutputDTO>))]
        public IActionResult Get()
        {
            var appResult = this.GetAllCommand.Execute();
            return this.Ok(appResult);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetProductColorTypeById"), ProducesResponseType(200, Type = typeof(ProductColorTypeGetByIdCommandOutputDTO))]
        public IActionResult Get(string id)
        {
            var result = this.GetByIdCommand.Execute(id);
            return this.Ok(result);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(ProductColorTypeInsertCommandInputDTO))]
        public IActionResult Post([FromBody]ProductColorTypeInsertCommandInputDTO model)
        {
            var appResult = this.InsertCommand.Execute(model);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

        /// <summary>
        /// Puts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPut(), ProducesResponseType(200, Type = typeof(ProductColorTypeUpdateCommandInputDTO))]
        public IActionResult Put([FromBody]ProductColorTypeUpdateCommandInputDTO model)
        {
            var appResult = this.UpdateCommand.Execute(model);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(200, Type = typeof(ProductColorTypeDeleteCommandOutputDTO))]
        public IActionResult Delete(string id)
        {
            var appResult = this.DeleteCommand.Execute(id);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }
    }
}
