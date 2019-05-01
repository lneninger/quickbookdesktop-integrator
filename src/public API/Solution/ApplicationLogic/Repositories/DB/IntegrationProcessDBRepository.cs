using ApplicationLogic.Business.Commands.InventoryAccount.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand.Models;
using DomainModel;
using Framework.Core.Messages;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using System.Collections.Generic;

namespace ApplicationLogic.Repositories.DB
{
    public interface IIntegrationProcessDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<IntegrationProcess>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<IntegrationProcess> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse Insert(IntegrationProcess entity);

        /// <summary>
        /// Delete InventoryItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse Delete(IntegrationProcess entity);
    }
}
