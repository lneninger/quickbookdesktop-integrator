using ApplicationLogic.Business.Commands.InventoryItem.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.PageQueryCommand.Models;
using DomainModel;
using Framework.Core.Messages;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using System.Collections.Generic;

namespace ApplicationLogic.Repositories.DB
{
    public interface IInventoryItemDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<InventoryItem>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<InventoryItem> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        OperationResponse<InventoryItem> GetByExternalId(string externalId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperationResponse<PageResult<InventoryItemPageQueryCommandOutputDTO>> PageQuery(PageQuery<InventoryItemPageQueryCommandInputDTO> input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        OperationResponse<DomainModel.InventoryItem> GetByFullName(string fullName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse Insert(InventoryItem entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //OperationResponse Update(AppUserUpdateCommandInputDTO input);

        /// <summary>
        /// Delete InventoryItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse Delete(InventoryItem entity);

        /// <summary>
        /// Logical Delete of an inventory item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse LogicalDelete(params InventoryItem[] entities);

        /// <summary>
        /// Gets the not in integration process inventory items.
        /// </summary>
        /// <param name="integrationProcessId">The integration process identifier.</param>
        /// <returns></returns>
        OperationResponse<IEnumerable<InventoryItem>> GetNotInIntegrationProcess(int integrationProcessId);
    }
}
