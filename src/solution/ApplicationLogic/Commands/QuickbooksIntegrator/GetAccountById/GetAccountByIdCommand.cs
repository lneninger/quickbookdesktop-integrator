using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById.Models;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using Framework.Autofac;
using System.Collections.Generic;

namespace ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById
{
    public class GetAccountByIdCommand : BaseIoCDisposable, IGetAccountByIdCommand
    {
        public GetAccountByIdCommand(IGeneralRepository repository)
        {
            this.Repository = repository;
        }

        public IGeneralRepository Repository { get; }

        public GetAccountByIdOutputDTO Execute(string id)
        {
            return this.Repository.GetAccountById(id);
        }
    }
}
