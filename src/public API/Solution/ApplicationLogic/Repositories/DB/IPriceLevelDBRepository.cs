using ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand.Models;
using DomainModel;
using Framework.Core.Messages;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using System.Collections.Generic;

namespace ApplicationLogic.Repositories.DB
{
    public interface IPriceLevelDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<PriceLevel>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<PriceLevel> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        OperationResponse<PriceLevel> GetByExternalId(string externalId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperationResponse<PageResult<PriceLevelPageQueryCommandOutputDTO>> PageQuery(PageQuery<PriceLevelPageQueryCommandInputDTO> input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        OperationResponse<DomainModel.PriceLevel> GetByFullName(string fullName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse Insert(PriceLevel entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //OperationResponse Update(AppUserUpdateCommandInputDTO input);

        /// <summary>
        /// Delete PriceLevel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse Delete(PriceLevel entity);

        /// <summary>
        /// Logical Delete of an inventory item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse LogicalDelete(PriceLevel entity);
    }
}
