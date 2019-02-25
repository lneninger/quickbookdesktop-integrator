using ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetPriceLevels
{
    public class GetPriceLevelsCommand : BaseIoCDisposable, IGetPriceLevelsCommand
    {
        public GetPriceLevelsCommand(IGeneralRepository repository)
        {
            this.Repository = repository;
        }

        public IGeneralRepository Repository { get; }

        public IEnumerable<GetPriceLevelsOutputPriceLevelItemDTO> Execute()
        {
            var items = this.Repository.PriceLevelGetAll();

            return items;
        }
    }
}
