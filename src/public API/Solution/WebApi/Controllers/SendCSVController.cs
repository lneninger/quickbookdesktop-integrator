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
        public IActionResult Categories()
        {
            var inventoryAccountResponse = this.InventoryAccountGetAllCommand.Execute();
            if (inventoryAccountResponse.Bag != null)
            {
                var list = inventoryAccountResponse.Bag;
                var map = list.Select(listItem => new Models.SendCSV.CategoryModel
                {
                    BranchID = 1002,
                    InCategoryID = listItem.ExternalId,
                    Description = listItem.Name
                }).ToList();

                var result = WriteCsvToMemory(map);
                var memoryStream = new MemoryStream(result);
                return File(memoryStream, "application/octet-stream", "categories.csv");
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
                var map = list.Select(listItem => new Models.SendCSV.InventoryItemModel
                {
                    BranchID = 1002,
                    InItemId = listItem.ExternalId,
                    ItemId = listItem.ExternalId,
                    ExCategoryId = listItem.InventoryAccountExternalId,
                    Price1 = listItem.Price,
                    QtyStock = listItem.Stock,
                }).ToList();

                var result = WriteCsvToMemory(map);
                var memoryStream = new MemoryStream(result);
                return File(memoryStream, "application/octet-stream", "us_item.csv");
            }

            return this.BadRequest();
        }

        [HttpGet("pricelevel")]
        public IActionResult PriceLevel()
        {
            var inventoryItemResponse = this.InventoryItemGetAllCommand.Execute();
            if (inventoryItemResponse.Bag != null)
            {
                var list = inventoryItemResponse.Bag;
                var map = new int[] { 0 }.Select(listItem => new Models.SendCSV.PriceLevelModel
                {
                    branchID = 1002,
                    InPriceLevelId = 0,
                    PriceLevel = 0,
                }).ToList();

                var result = WriteCsvToMemory(map);
                var memoryStream = new MemoryStream(result);
                return File(memoryStream, "application/octet-stream", "us_pricelevel.csv");
            }

            return this.BadRequest();
        }


        [HttpGet("itempricelevel")]
        public IActionResult ItemPriceLevel()
        {
            var inventoryItemResponse = this.InventoryItemGetAllCommand.Execute();
            if (inventoryItemResponse.Bag != null)
            {
                var list = inventoryItemResponse.Bag;
                var map = list.Select(listItem => new Models.SendCSV.ItemPriceLevelModel
                {
                    branchID = 1002,
                    ExItemId = listItem.ExternalId,
                    ExPriceLeverlId = 0,
                    Price = listItem.Price,
                }).ToList();

                var result = WriteCsvToMemory(map);
                var memoryStream = new MemoryStream(result);
                return File(memoryStream, "application/octet-stream", "us_pricelevel.csv");
            }

            return this.BadRequest();
        }
        public byte[] WriteCsvToMemory<T>(IEnumerable<T> records)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}
