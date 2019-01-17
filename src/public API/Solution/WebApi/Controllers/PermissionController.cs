using ApplicationLogic.Business.Commands.AppUser.DeleteCommand;
using ApplicationLogic.Business.Commands.AppUser.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.GetAllCommand;
using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand;
using ApplicationLogic.Business.Commands.AppUser.PageQueryCommand;
//using ApplicationLogic.Business.Commands.AppUser.RegisterCommand;
//using ApplicationLogic.Business.Commands.AppUser.UpdateCommand;
using ApplicationLogic.Business.Commands.Security;
using ApplicationLogic.SignalR;
using DomainModel.Identity;
using Framework.Core.Messages;
using Framework.Web.Helpers;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
//using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Authorization = Microsoft.AspNetCore.Authorization;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// AppUser API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/permission")]
    public class PermissionController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="registerCommand">The register command</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public PermissionController(/*IHubContext<GlobalHub> hubContext, */UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAppUserPageQueryCommand pageQueryCommand, IAppUserGetAllCommand getAllCommand, IAppUserGetByIdCommand getByIdCommand/*, IAppUserRegisterCommand registerCommand, IAppUserUpdateCommand updateCommand*/, IAppUserDeleteCommand deleteCommand):base(/*hubContext*/)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public UserManager<AppUser> UserManager { get; }

        /// <summary>
        /// 
        /// </summary>
        public RoleManager<IdentityRole> RoleManager { get; }

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

        ///// <summary>
        ///// Gets the update command.
        ///// </summary>
        ///// <value>
        ///// The update command.
        ///// </value>
        //public IAppUserUpdateCommand UpdateCommand { get; }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public IAppUserDeleteCommand DeleteCommand { get; }

        /// <summary>
        /// Posts the user manager.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AppUserClaimInsertCommandInputDTO input)
        {
            var result = new OperationResponse<AppUserClaimInsertCommandInputDTO>();
            if (!ModelState.IsValid)
            {
                result.AddModelState(ModelState);
                //return BadRequest(ModelState);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(input.UserId))
                {
                    var user = await this.UserManager.FindByIdAsync(input.UserId);
                    if (user != null)
                    {
                        await this.UserManager.AddClaimAsync(user, new System.Security.Claims.Claim(RiverdaleMainApp2_0.Auth.Constants.Strings.JwtClaimIdentifiers.Permissions, input.permissionId));
                    }
                    else
                    {
                        return this.BadRequest(new OperationResponse().AddError("User not found"));
                    }
                }

                else if (!string.IsNullOrWhiteSpace(input.RoleId))
                {
                    var role = await this.RoleManager.FindByIdAsync(input.RoleId);
                    if (role != null)
                    {
                        await this.RoleManager.AddClaimAsync(role, new System.Security.Claims.Claim(RiverdaleMainApp2_0.Auth.Constants.Strings.JwtClaimIdentifiers.Permissions, input.permissionId));
                    }
                    else
                    {
                        return this.BadRequest(new OperationResponse().AddError("Role not found"));
                    }
                }
            }

            return new OkObjectResult("Account created");
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete, ProducesResponseType(200, Type = typeof(AppUserDeleteCommandOutputDTO))]
        public async Task<IActionResult> Delete([FromBody]AppUserClaimDeleteCommandInputDTO input)
        {
            var result = new OperationResponse<AppUserClaimDeleteCommandInputDTO>();
            if (!ModelState.IsValid)
            {
                result.AddModelState(ModelState);
                //return BadRequest(ModelState);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(input.UserId))
                {
                    var user = await this.UserManager.FindByIdAsync(input.UserId);
                    if (user != null)
                    {
                        await this.UserManager.RemoveClaimAsync(user, new System.Security.Claims.Claim(RiverdaleMainApp2_0.Auth.Constants.Strings.JwtClaimIdentifiers.Permissions, input.Claim));
                    }
                    else
                    {
                        return this.BadRequest(new OperationResponse().AddError("User not found"));
                    }
                }

                else if (!string.IsNullOrWhiteSpace(input.RoleId))
                {
                    var role = await this.RoleManager.FindByIdAsync(input.RoleId);
                    if (role != null)
                    {
                        await this.RoleManager.RemoveClaimAsync(role, new System.Security.Claims.Claim(RiverdaleMainApp2_0.Auth.Constants.Strings.JwtClaimIdentifiers.Permissions, input.Claim));
                    }
                    else
                    {
                        return this.BadRequest(new OperationResponse().AddError("Role not found"));
                    }
                }
            }
            return new OkObjectResult("Account created");
        }
    }
}
