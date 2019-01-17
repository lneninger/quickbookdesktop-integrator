using ApplicationLogic.Business.Commands.FunzaIntegrator.AuthenticateCommand;
using ApplicationLogic.Business.Commands.FunzaIntegrator.AuthenticateCommand.Models;
using ApplicationLogic.Business.Commands.FunzaIntegrator.GetProductsCommand;
using ApplicationLogic.SignalR;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RiverdaleMainApp2_0.Auth;
using System.Collections.Generic;
using Authorization = Microsoft.AspNetCore.Authorization;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// Product API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/funza")]
    public class FunzaController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="insertCommand">The insert command.</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public FunzaController(IHubContext<GlobalHub, IGlobalHub> hubContext, IFunzaAuthenticateCommand authenticateCommand, IFunzaGetProductsCommand getProductsCommand) :base()
        {
            this.SignalRHubContext = hubContext;
            this.AuthenticateCommand = authenticateCommand;
            this.GetProductsCommand = getProductsCommand;
            
        }

        /// <summary>
        /// 
        /// </summary>
        public IFunzaAuthenticateCommand AuthenticateCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public IFunzaGetProductsCommand GetProductsCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public IHubContext<GlobalHub, IGlobalHub> SignalRHubContext { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(200, Type = typeof(PageResult<FunzaAuthenticateCommandOutputDTO>))]
        [Route("authenticate")]
        public IActionResult Authenticate()
        {
            var result = this.AuthenticateCommand.Execute();
            var productsTest = this.GetProductsCommand.Execute();

            return this.Ok(result);
        }

    }
}
