using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems
{
    public interface IGetInventoryItemsCommand : ICommandAction<IEnumerable<GetInventoryItemsOutputIventoryItemDTO>>
    {
    }
}
