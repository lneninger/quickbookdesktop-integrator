using ApplicationLogic.Business.Commands.AppUser.DeleteCommand;
using ApplicationLogic.Business.Commands.AppUser.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.GetAllCommand;
using ApplicationLogic.Business.Commands.AppUser.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand;
using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.PageQueryCommand;
using ApplicationLogic.Business.Commands.AppUser.PageQueryCommand.Models;
//using ApplicationLogic.Business.Commands.AppUser.RegisterCommand;
//using ApplicationLogic.Business.Commands.AppUser.RegisterCommand.Models;
//using ApplicationLogic.Business.Commands.AppUser.UpdateCommand;
//using ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models;
using ApplicationLogic.Business.Commands.Security;
using ApplicationLogic.SignalR;
using DomainModel.Identity;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using RiverdaleMainApp2_0.Auth;
using RiverdaleMainApp2_0.Auth.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization = Microsoft.AspNetCore.Authorization;
using ApplicationLogic.Business.Commands.AppUser.UpdateCommand;
using ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models;
using System;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// AppUser API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        ///// <param name="hubContext"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="registerCommand">The register command</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public UserController(/*IHubContext<GlobalHub> hubContext, */UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAppUserPageQueryCommand pageQueryCommand, IAppUserGetAllCommand getAllCommand, IAppUserGetByIdCommand getByIdCommand/*, IAppUserRegisterCommand registerCommand*/, IAppUserUpdateCommand updateCommand, IAppUserDeleteCommand deleteCommand):base(/*hubContext*/)
        {
            this.RoleManager = roleManager;
            this.UserManager = userManager;
            this.PageQueryCommand = pageQueryCommand;
            this.GetAllCommand = getAllCommand;
            this.GetByIdCommand = getByIdCommand;
            //this.RegisterCommand = registerCommand;
            this.UpdateCommand = updateCommand;
            this.DeleteCommand = deleteCommand;
        }

        /// <summary>
        /// 
        /// </summary>
        public RoleManager<IdentityRole> RoleManager { get; }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public UserManager<AppUser> UserManager { get; }

        /// <summary>
        /// Gets the get all command.
        /// </summary>
        /// <value>
        /// The get all command.
        /// </value>
        public IAppUserGetAllCommand GetAllCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public IAppUserPageQueryCommand PageQueryCommand { get; }


        /// <summary>
        /// Gets the get by identifier command.
        /// </summary>
        /// <value>
        /// The get by identifier command.
        /// </value>
        public IAppUserGetByIdCommand GetByIdCommand { get; }

        ///// <summary>
        ///// Gets the insert command.
        ///// </summary>
        ///// <value>
        ///// The insert command.
        ///// </value>
        //public IAppUserRegisterCommand RegisterCommand { get; }

        /// <summary>
        /// Gets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public IAppUserUpdateCommand UpdateCommand { get; }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public IAppUserDeleteCommand DeleteCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(PageResult<AppUserPageQueryCommandOutputDTO>))]
        [Route("pagequery")]
        [Authorization.Authorize(Policy = PermissionsEnum.UserRole_Read)]
        public IActionResult PageQuery([FromBody]PageQuery<AppUserPageQueryCommandInputDTO> input)
        {
            var result = this.PageQueryCommand.Execute(input);
            return this.Ok(result);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet(""), ProducesResponseType(200, Type = typeof(IEnumerable<AppUserGetAllCommandOutputDTO>))]
        [Authorization.Authorize(Policy = PermissionsEnum.UserRole_Read)]
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
        [HttpGet("{id}"), ProducesResponseType(200, Type = typeof(AppUserGetByIdCommandOutputDTO))]
        [Authorization.Authorize(Policy = PermissionsEnum.UserRole_Read)]
        public IActionResult Get(string id)
        {
            var result = this.GetByIdCommand.Execute(id);
            return this.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut, ProducesResponseType(200, Type = typeof(AppUserUpdateCommandOutputDTO))]
        [Authorization.Authorize(Policy = PermissionsEnum.UserRole_Modify), Authorization.Authorize(Policy = PermissionsEnum.UserRole_Manage)]
        public IActionResult Update([FromBody]AppUserUpdateCommandInputDTO model)
        {
            var result = this.UpdateCommand.Execute(model);
            return result.IsSucceed ? (IActionResult)this.Ok(result) : (IActionResult)this.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("updatePassword"), ProducesResponseType(200, Type = typeof(AppUserUpdateCommandOutputDTO))]
        [Authorization.Authorize(Policy = PermissionsEnum.UserRole_Modify), Authorization.Authorize(Policy = PermissionsEnum.UserRole_Manage)]
        public async Task<IActionResult> UpdatePassword([FromBody]AppUserUpdatePasswordCommandInputDTO model)
        {
            var result = new OperationResponse();
            var user = await this.UserManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                try
                {
                    var resetPasswordToken = await this.UserManager.GeneratePasswordResetTokenAsync(user);
                    await this.UserManager.ResetPasswordAsync(user, resetPasswordToken, model.Password);
                }
                catch (Exception ex)
                {
                    result.AddException($"Error reseting password", ex);
                }
            }
            else
            {
                result.AddError("User not found");
            }

            return result.IsSucceed ? (IActionResult)this.Ok(result) : (IActionResult)this.BadRequest(result);
        }

        //  /// <summary>
        ///// Posts the user manager.
        ///// </summary>
        ///// <param name="input">The input.</param>
        ///// <returns></returns>
        //[HttpPost]
        //[HttpPost("register")]
        //[Authorization.Authorize(Policy = PermissionsEnum.UserRole_Manage)]
        //public async Task<IActionResult> Post([FromBody]AppUserRegisterCommandInputDTO input)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userIdentity = new AppUser
        //    {
        //        Email = input.Email,
        //        UserName = input.UserName,
        //        FirstName = input.FirstName,
        //        LastName = input.LastName,
        //        PictureUrl = input.PictureUrl
        //    };

        //    var result = await this.UserManager.CreateAsync(userIdentity, input.Password);

        //    if (!result.Succeeded)
        //        return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));


        //    return new OkObjectResult("Account created");
        //}


        ///// <summary>
        ///// Puts the specified model.
        ///// </summary>
        ///// <param name="model">The model.</param>
        //[HttpPut(), ProducesResponseType(200, Type = typeof(AppUserUpdateCommandOutputDTO))]
        //[Authorization.Authorize(Policy = PermissionsEnum.UserRole_Manage), Authorization.Authorize(Policy = PermissionsEnum.UserRole_Modify)]
        //public IActionResult Put([FromBody]AppUserUpdateCommandInputDTO model)
        //{
        //    var appResult = this.UpdateCommand.Execute(model);
        //    return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        //}

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(200, Type = typeof(AppUserDeleteCommandOutputDTO))]
        [Authorization.Authorize(Policy = PermissionsEnum.UserRole_Manage)]
        public IActionResult Delete(string id)
        {
            var appResult = this.DeleteCommand.Execute(id);
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("addrole"), ProducesResponseType(200, Type = typeof(OperationResponse<List<string>>))]
        [Authorization.Authorize(Policy = PermissionsEnum.UserRole_Manage)]
        public async Task<IActionResult> AddRole([FromBody]AppUserRoleRelationInsertCommandInputDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityRole role = null;
            var result = new OperationResponse<IList<string>>();
            var user = await this.UserManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                result.AddError("User not found");
            }

            if (result.IsSucceed)
            {
                role = await this.RoleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    result.AddError("Role not found");
                }
            }

            if (result.IsSucceed)
            {
                try
                {
                    await this.UserManager.AddToRoleAsync(user, role.Name);

                    result.Bag = await this.UserManager.GetRolesAsync(user);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(result.AddException($"Error adding role {role.Name} to user {user.UserName}", ex));
                }
            }

            return result.IsSucceed ? (IActionResult)this.Ok(result) : (IActionResult)this.BadRequest(result);
        }
    }
}
