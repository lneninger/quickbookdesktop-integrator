using ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels
{
    public interface IGetPriceLevelsCommand : ICommandAction<IEnumerable<GetPriceLevelsOutputPriceLevelItemDTO>>
    {
    }
}
