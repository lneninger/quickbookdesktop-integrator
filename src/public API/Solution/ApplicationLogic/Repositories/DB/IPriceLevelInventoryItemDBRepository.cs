//using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.PageQueryCommand.Models;
using DomainModel;
using Framework.Core.Messages;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using System.Collections.Generic;

namespace ApplicationLogic.Repositories.DB
{
    public interface IPriceLevelInventoryItemDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<PriceLevelInventoryItem>> GetAll();


        OperationResponse<IEnumerable<PriceLevelInventoryItem>> GetAllWithPriceLevel();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<PriceLevelInventoryItem> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        OperationResponse<PriceLevelInventoryItem> GetByExternalIdAndPriceLevel(string externalId, string priceLevelExternalId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperationResponse<PageResult<PriceLevelInventoryItemPageQueryCommandOutputDTO>> PageQuery(PageQuery<PriceLevelInventoryItemPageQueryCommandInputDTO> input);

        /// <summary>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse Insert(PriceLevelInventoryItem entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //OperationResponse Update(AppUserUpdateCommandInputDTO input);

        /// <summary>
        /// Delete PriceLevelInventoryItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //OperationResponse Delete(PriceLevelInventoryItem entity);

        ///// <summary>
        ///// Logical Delete of an inventory item
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //OperationResponse LogicalDelete(PriceLevelInventoryItem entity);
    }
}
