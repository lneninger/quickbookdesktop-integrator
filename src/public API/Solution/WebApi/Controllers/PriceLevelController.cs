using ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand;
using ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand;
using ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand;
using ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.InsertCommand;
using ApplicationLogic.Business.Commands.PriceLevel.InsertCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand;
using ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand;
using ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand.Models;
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
    /// PriceLevel API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/PriceLevel")]
    public class PriceLevelController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriceLevelController"/> class.
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="insertCommand">The insert command.</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public PriceLevelController(IHubContext<GlobalHub, IGlobalHub> hubContext, IPriceLevelPageQueryCommand pageQueryCommand, IPriceLevelGetAllCommand getAllCommand, IPriceLevelGetByIdCommand getByIdCommand, IPriceLevelUpdateCommand updateCommand, IPriceLevelDeleteCommand deleteCommand): base(/*hubContext*/)
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
        public IPriceLevelGetAllCommand GetAllCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public IHubContext<GlobalHub, IGlobalHub> SignalRHubContext { get; }

        /// <summary>
        /// 
        /// </summary>
        public IPriceLevelPageQueryCommand PageQueryCommand { get; }


        /// <summary>
        /// Gets the get by identifier command.
        /// </summary>
        /// <value>
        /// The get by identifier command.
        /// </value>
        public IPriceLevelGetByIdCommand GetByIdCommand { get; }

        /// <summary>
        /// Gets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public IPriceLevelUpdateCommand UpdateCommand { get; }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public IPriceLevelDeleteCommand DeleteCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(PageResult<PriceLevelPageQueryCommandOutputDTO>))]
        [Route("pagequery")]
        public IActionResult PageQuery([FromBody]PageQuery<PriceLevelPageQueryCommandInputDTO> input)
        {
            var result = this.PageQueryCommand.Execute(input);

            return this.Ok(result);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet(""), ProducesResponseType(200, Type = typeof(IEnumerable<PriceLevelGetAllCommandOutputDTO>))]
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
        [HttpGet("{id}"), ProducesResponseType(200, Type = typeof(PriceLevelGetByIdCommandOutputDTO))]
        public IActionResult Get(int id)
        {
            var result = this.GetByIdCommand.Execute(id);

            return this.Ok(result);
        }

        /// <summary>
        /// Puts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPut(), ProducesResponseType(200, Type = typeof(PriceLevelUpdateCommandOutputDTO))]
        [Authorization.Authorize(Policy = PermissionsEnum.PriceLevel_Modify, Roles = Constants.Strings.JwtClaims.Administrator)]
        public IActionResult Put([FromBody]PriceLevelUpdateCommandInputDTO model)
        {
            var appResult = this.UpdateCommand.Execute(model);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(200, Type = typeof(PriceLevelDeleteCommandOutputDTO))]
        public IActionResult Delete(int id)
        {
            var appResult = this.DeleteCommand.Execute(id);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }
    }
}
