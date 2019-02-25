using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds
{
    public class GetAccountByIdsCommand : BaseIoCDisposable, IGetAccountByIdsCommand
    {
        public GetAccountByIdsCommand(IGeneralRepository repository)
        {
            this.Repository = repository;
        }

        public IGeneralRepository Repository { get; }

        public List<GetAccountByIdsOutputDTO> Execute(List<string> ids)
        {
            return this.Repository.GetAccountById(ids).ToList();
        }
    }
}
