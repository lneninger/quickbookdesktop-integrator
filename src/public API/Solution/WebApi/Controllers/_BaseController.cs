using ApplicationLogic.Business.Commands.CustomerFreightout.DeleteCommand;
using ApplicationLogic.Business.Commands.CustomerFreightout.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.CustomerFreightout.GetAllCommand;
using ApplicationLogic.Business.Commands.CustomerFreightout.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.CustomerFreightout.GetByIdCommand;
using ApplicationLogic.Business.Commands.CustomerFreightout.InsertCommand;
using ApplicationLogic.Business.Commands.CustomerFreightout.InsertCommand.Models;
using ApplicationLogic.Business.Commands.CustomerFreightout.PageQueryCommand;
using ApplicationLogic.Business.Commands.CustomerFreightout.PageQueryCommand.Models;
using ApplicationLogic.Business.Commands.CustomerFreightout.UpdateCommand;
using ApplicationLogic.Business.Commands.CustomerFreightout.UpdateCommand.Models;
using ApplicationLogic.SignalR;
using CommunicationModel;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Logging.Log4Net;
//using Microsoft.AspNet.SignalR;
//using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace RiverdaleMainApp2_0.Controllers
{
    /// <summary>
    /// Customer API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController(/*IHubContext<GlobalHub> hubContext*/): base()
        {
            //this.HubContext = hubContext;
        }

        //public IHubContext<GlobalHub> HubContext { get; }
    }
}