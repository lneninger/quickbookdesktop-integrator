using ApplicationLogic.Business.Commands.InventoryAccount.GetAllCommand;
using ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand;
using ApplicationLogic.SignalR;
using CsvHelper;
using CsvHelper.Fluent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickbooksIntegratorAPI.Controllers
{
    /// <summary>
    /// InventoryItem API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/sendcsv")]
    public class SendCSVController : BaseController
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
        public SendCSVController(IHubContext<GlobalHub, IGlobalHub> hubContext, IInventoryAccountGetAllCommand inventoryAccountGetAllCommand, IInventoryItemGetAllCommand inventoryItemGetAllCommand) : base(/*hubContext*/)
        {
            this.SignalRHubContext = hubContext;
            this.InventoryAccountGetAllCommand = inventoryAccountGetAllCommand;
            this.InventoryItemGetAllCommand = inventoryItemGetAllCommand;
        }

        public IHubContext<GlobalHub, IGlobalHub> SignalRHubContext { get; }
        public IInventoryAccountGetAllCommand InventoryAccountGetAllCommand { get; }
        public IInventoryItemGetAllCommand InventoryItemGetAllCommand { get; }

        [HttpGet("categories")]
        public IActionResult Categories() {
            var inventoryAccountResponse = this.InventoryAccountGetAllCommand.Execute();
            if (inventoryAccountResponse.Bag != null)
            {
                var list = inventoryAccountResponse.Bag;
                var map = list.Select(listItem => new Models.SendCSV.Category
                {
                    BranchID = 1002,
                    InCategoryID = listItem.ExternalId,
                    Description = listItem.Name
                }).ToList();

                using (var stream = new MemoryStream())
                {
                    map.WriteCsv(stream);
                    return File(stream, "application/octet-stream", "categories.csv");
                }
            }

            return this.BadRequest();
        }

        [HttpGet("items")]
        public IActionResult Items()
        {
            var inventoryItemResponse = this.InventoryItemGetAllCommand.Execute();
            if (inventoryItemResponse.Bag != null)
            {
                var list = inventoryItemResponse.Bag;
                var map = list.Select(listItem => new Models.SendCSV.InventoryItem
                {
                    BranchID = 1002,
                    InItemId = listItem.ExternalId,
                    ItemId = listItem.ExternalId,
                    ExCategoryId = listItem.InventoryAccountExternalId,
                    Price1 = listItem.Price,
                    QtyStock = listItem.Stock,
                }).ToList();

                var stream = new MemoryStream();
                map.WriteCsv(stream);
                return File(stream, "application/octet-stream", "categories.csv");
            }

            return this.BadRequest();
        }


    }
}
