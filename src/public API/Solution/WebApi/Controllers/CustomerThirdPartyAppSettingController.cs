using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.DeleteCommand;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetAllCommand;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetByIdCommand;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.InsertCommand;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.InsertCommand.Models;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.PageQueryCommand;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.PageQueryCommand.Models;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand;
using ApplicationLogic.Business.Commands.CustomerThirdPartyAppSetting.UpdateCommand.Models;
using ApplicationLogic.SignalR;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Authorization = Microsoft.AspNetCore.Authorization;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// CustomerThirdPartyAppSetting API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/CustomerThirdPartyAppSetting")]
    public class CustomerThirdPartyAppSettingController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerThirdPartyAppSettingController"/> class.
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="insertCommand">The insert command.</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public CustomerThirdPartyAppSettingController(/*IHubContext<GlobalHub> hubContext, */ICustomerThirdPartyAppSettingPageQueryCommand pageQueryCommand, ICustomerThirdPartyAppSettingGetAllCommand getAllCommand, ICustomerThirdPartyAppSettingGetByIdCommand getByIdCommand, ICustomerThirdPartyAppSettingInsertCommand insertCommand, ICustomerThirdPartyAppSettingUpdateCommand updateCommand, ICustomerThirdPartyAppSettingDeleteCommand deleteCommand):base(/*hubContext*/)
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
        public ICustomerThirdPartyAppSettingGetAllCommand GetAllCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICustomerThirdPartyAppSettingPageQueryCommand PageQueryCommand { get; }


        /// <summary>
        /// Gets the get by identifier command.
        /// </summary>
        /// <value>
        /// The get by identifier command.
        /// </value>
        public ICustomerThirdPartyAppSettingGetByIdCommand GetByIdCommand { get; }

        /// <summary>
        /// Gets the insert command.
        /// </summary>
        /// <value>
        /// The insert command.
        /// </value>
        public ICustomerThirdPartyAppSettingInsertCommand InsertCommand { get; }

        /// <summary>
        /// Gets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public ICustomerThirdPartyAppSettingUpdateCommand UpdateCommand { get; }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public ICustomerThirdPartyAppSettingDeleteCommand DeleteCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(PageResult<CustomerThirdPartyAppSettingPageQueryCommandOutputDTO>))]
        [Route("pagequery")]
        public IActionResult PageQuery([FromBody]PageQuery<CustomerThirdPartyAppSettingPageQueryCommandInputDTO> input)
        {
            var result = this.PageQueryCommand.Execute(input);
            return this.Ok(result);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name = "GetAllCustomerThirdPartyAppSetting"), ProducesResponseType(200, Type = typeof(CustomerThirdPartyAppSettingGetAllCommandOutputDTO))]
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
        [HttpGet("{id}", Name = "GetCustomerThirdPartyAppSettingById"), ProducesResponseType(200, Type = typeof(CustomerThirdPartyAppSettingGetByIdCommandOutputDTO))]
        public IActionResult Get(int id)
        {
            var appResult = this.GetByIdCommand.Execute(id);

            return this.Ok(appResult);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(CustomerThirdPartyAppSettingInsertCommandOutputDTO))]
        public IActionResult Post([FromBody]CustomerThirdPartyAppSettingInsertCommandInputDTO model)
        {
            var appResult = this.InsertCommand.Execute(model);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

        /// <summary>
        /// Puts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPut(), ProducesResponseType(200, Type = typeof(CustomerThirdPartyAppSettingUpdateCommandOutputDTO))]
        public IActionResult Put([FromBody]CustomerThirdPartyAppSettingUpdateCommandInputDTO model)
        {
            var appResult = this.UpdateCommand.Execute(model);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(200, Type = typeof(CustomerThirdPartyAppSettingDeleteCommandOutputDTO))]
        public IActionResult Delete(int id)
        {
            var appResult = this.DeleteCommand.Execute(id);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

    }
}
