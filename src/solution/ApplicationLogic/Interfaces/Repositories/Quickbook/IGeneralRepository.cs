using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces.Repositories.Quickbooks
{
    public interface IGeneralRepository
    {
        IEnumerable<GetInventoryItemsOutputIventoryItemDTO> InventoryItemGetAll();

        GetAccountByIdOutputDTO GetAccountById(string id);

        IEnumerable<GetAccountByIdsOutputDTO> GetAccountById(IEnumerable<string> ids);

        IEnumerable<GetPriceLevelsOutputPriceLevelItemDTO> PriceLevelGetAll();
    }
}
