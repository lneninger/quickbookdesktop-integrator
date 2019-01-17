using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand;
using ApplicationLogic.Business.Commands.Customer.DeleteCommand;
using ApplicationLogic.Business.Commands.Customer.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.Customer.GetAllCommand;
using ApplicationLogic.Business.Commands.Customer.GetByIdCommand;
using ApplicationLogic.Business.Commands.Customer.InsertCommand;
using ApplicationLogic.Business.Commands.Customer.InsertCommand.Models;
using ApplicationLogic.Business.Commands.Customer.PageQueryCommand;
using ApplicationLogic.Business.Commands.Customer.PageQueryCommand.Models;
using ApplicationLogic.Business.Commands.Customer.UpdateCommand;
using ApplicationLogic.Business.Commands.Customer.UpdateCommand.Models;
using ApplicationLogic.Business.Commands.Security;
using ApplicationLogic.SignalR;
using CommunicationModel;
using DomainDatabaseMapping;
using DomainModel.Identity;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
//using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RiverdaleMainApp2_0.Auth;
using RiverdaleMainApp2_0.Auth.Helpers;
using RiverdaleMainApp2_0.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// Customer API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/accounts")]
    public class AccountsController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="jwtOptions"></param>
        public AccountsController(/*IHubContext<GlobalHub> hubContext, */UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions/*, IAppUserRegisterCommand appUserRegisterCommand, IAppUserAuthenticateCommand appUserAuthenticateCommand, IAppUserGetByIdCommand appUserGetByIdCommand*/):base(/*hubContext*/)
        {
            this.RoleManager = roleManager;
            this.UserManager = userManager;
            this.JwtFactory = jwtFactory;
            this.JwtOptions = jwtOptions.Value;
        }


        /// <summary>
        /// Gets the role manager.
        /// </summary>
        /// <value>
        /// The role manager.
        /// </value>
        public RoleManager<IdentityRole> RoleManager { get; }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public UserManager<AppUser> UserManager { get; }

        /// <summary>
        /// The JWT factory
        /// </summary>
        public IJwtFactory JwtFactory;

        /// <summary>
        /// The JWT options
        /// </summary>
        public JwtIssuerOptions JwtOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AppUserAuthenticateCommandInputDTO input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(input.UserName, input.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var id = identity.Claims.Single(c => c.Type == "id").Value;

            return await this.GenerateAuthenticationInfo(id);
        }

        /// <summary>
        /// Get Authentication info
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("authenticationInfo")]
        public async Task<IActionResult> AuthenticationInfo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // alternatively
            var idClaim = claimsIdentity.FindFirst("id");
            if (idClaim != null)
            {
                // get claim containing user id
                var id = idClaim.Value;
                return await this.GenerateAuthenticationInfo(id);
            }

            return BadRequest("No authentication data");
        }

        private async Task<IActionResult> GenerateAuthenticationInfo(string id)
        {
            var dbUser = await this.UserManager.FindByIdAsync(id);
            var identity = await Task.FromResult(this.JwtFactory.GenerateClaimsIdentity(dbUser.UserName, id));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, id));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, dbUser.UserName));

            var claims = (await this.UserManager.GetClaimsAsync(dbUser)).AsEnumerable();
            var roleNames = await this.UserManager.GetRolesAsync(dbUser);
            foreach (var roleName in roleNames)
            {
                claims = claims.Concat((await this.RoleManager.GetClaimsAsync(await this.RoleManager.FindByNameAsync(roleName))).ToList());
            }

            var permissions = claims.Where(claim => claim.Type == RiverdaleMainApp2_0.Auth.Constants.Strings.JwtClaimIdentifiers.Permissions);
            permissions.ToList().ForEach(permission =>
            {
                identity.AddClaim(permission);
            });

            return Ok(new
            {
                UserName = dbUser.UserName,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                AccessToken = await this.JwtFactory.GenerateEncodedToken(dbUser.UserName, identity),//identity.tokenString,
                PictureUrl = dbUser.PictureUrl,
                permissions = permissions.Select(o => o.Value),
                ExpiresAt = this.JwtOptions.Expiration,
                ExpiresIn = this.JwtOptions.ValidFor.TotalSeconds
            });
        }

        [NonAction]
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await this.UserManager.FindByNameAsync(userName);
                if (userToVerify == null)
                {
                    userToVerify = await this.UserManager.FindByEmailAsync(userName);
                }

                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await this.UserManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(this.JwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
