using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds
{
    public interface IGetAccountByIdsCommand : ICommandFunc<List<string>, List<GetAccountByIdsOutputDTO>>
    {
    }
}
