using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces.Repositories.Quickbooks
{
    public interface IInventoryItemRepository
    {
        IEnumerable<GetInventoryItemsOutputIventoryItemDTO> GetAll();

        GetAccountByIdOutputDTO GetAccountById(string id);

        IEnumerable<GetAccountByIdsOutputDTO> GetAccountById(IEnumerable<string> ids);
    }
}
