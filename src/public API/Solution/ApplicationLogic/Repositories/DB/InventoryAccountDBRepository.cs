using ApplicationLogic.Business.Commands.InventoryAccount.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand.Models;
using DomainModel;
using Framework.Core.Messages;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using System.Collections.Generic;

namespace ApplicationLogic.Repositories.DB
{
    public interface IInventoryAccountDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<InventoryAccount>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<InventoryAccount> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        OperationResponse<InventoryAccount> GetByExternalId(string externalId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperationResponse<PageResult<InventoryAccountPageQueryCommandOutputDTO>> PageQuery(PageQuery<InventoryAccountPageQueryCommandInputDTO> input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        OperationResponse<DomainModel.InventoryAccount> GetByFullName(string fullName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse Insert(InventoryAccount entity);

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
        OperationResponse Delete(InventoryAccount entity);

        /// <summary>
        /// Logical Delete of an inventory item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse LogicalDelete(InventoryAccount entity);
    }
}
