using ApplicationLogic.Business.Commands.InventoryItem.DeleteCommand;
using ApplicationLogic.Business.Commands.InventoryItem.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand;
using ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand;
using ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand;
using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.PageQueryCommand;
using ApplicationLogic.Business.Commands.InventoryItem.PageQueryCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.UpdateCommand;
using ApplicationLogic.Business.Commands.InventoryItem.UpdateCommand.Models;
using ApplicationLogic.SignalR;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuickbooksIntegratorAPI.Auth;
using System.Collections.Generic;
using Authorization = Microsoft.AspNetCore.Authorization;

namespace QuickbooksIntegratorAPI.Controllers
{
    /// <summary>
    /// InventoryItem API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/InventoryItem")]
    public class InventoryItemController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemController"/> class.
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="insertCommand">The insert command.</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public InventoryItemController(IHubContext<GlobalHub, IGlobalHub> hubContext, IInventoryItemPageQueryCommand pageQueryCommand, IInventoryItemGetAllCommand getAllCommand, IInventoryItemGetByIdCommand getByIdCommand, IInventoryItemUpdateCommand updateCommand, IInventoryItemDeleteCommand deleteCommand): base(/*hubContext*/)
        {
            this.SignalRHubContext = hubContext;
            this.PageQueryCommand = pageQueryCommand;
            this.GetAllCommand = getAllCommand;
            this.GetByIdCommand = getByIdCommand;
            this.UpdateCommand = updateCommand;
            this.DeleteCommand = deleteCommand;
        }

        /// <summary>
        /// Gets the get all command.
        /// </summary>
        /// <value>
        /// The get all command.
        /// </value>
        public IInventoryItemGetAllCommand GetAllCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public IHubContext<GlobalHub, IGlobalHub> SignalRHubContext { get; }

        /// <summary>
        /// 
        /// </summary>
        public IInventoryItemPageQueryCommand PageQueryCommand { get; }


        /// <summary>
        /// Gets the get by identifier command.
        /// </summary>
        /// <value>
        /// The get by identifier command.
        /// </value>
        public IInventoryItemGetByIdCommand GetByIdCommand { get; }

        /// <summary>
        /// Gets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public IInventoryItemUpdateCommand UpdateCommand { get; }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public IInventoryItemDeleteCommand DeleteCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(PageResult<InventoryItemPageQueryCommandOutputDTO>))]
        [Route("pagequery")]
        public IActionResult PageQuery([FromBody]PageQuery<InventoryItemPageQueryCommandInputDTO> input)
        {
            var result = this.PageQueryCommand.Execute(input);

            return this.Ok(result);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet(""), ProducesResponseType(200, Type = typeof(IEnumerable<InventoryItemGetAllCommandOutputDTO>))]
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
        [HttpGet("{id}"), ProducesResponseType(200, Type = typeof(InventoryItemGetByIdCommandOutputDTO))]
        public IActionResult Get(int id)
        {
            var result = this.GetByIdCommand.Execute(id);

            return this.Ok(result);
        }

        /// <summary>
        /// Puts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPut(), ProducesResponseType(200, Type = typeof(InventoryItemUpdateCommandOutputDTO))]
        [Authorization.Authorize(Policy = PermissionsEnum.InventoryItem_Modify, Roles = Constants.Strings.JwtClaims.Administrator)]
        public IActionResult Put([FromBody]InventoryItemUpdateCommandInputDTO model)
        {
            var appResult = this.UpdateCommand.Execute(model);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(200, Type = typeof(InventoryItemDeleteCommandOutputDTO))]
        public IActionResult Delete(int id)
        {
            var appResult = this.DeleteCommand.Execute(id);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }
    }
}
