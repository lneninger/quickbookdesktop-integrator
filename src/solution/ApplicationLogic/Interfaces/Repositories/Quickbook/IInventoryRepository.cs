using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces.Repositories.Quickbooks
{
    public interface IInventoryRepository
    {
        IEnumerable<GetInventoryItemsOutputIventoryItemDTO> GetAll();
    }
}
