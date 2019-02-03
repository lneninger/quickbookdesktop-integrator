using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById
{
    public interface IGetAccountByIdCommand : ICommandFunc<string, GetAccountByIdOutputDTO>
    {
    }
}
