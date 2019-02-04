using ApplicationLogic.Business.Commands.IncomeAccount.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand.Models;
using DomainModel;
using Framework.Core.Messages;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using System.Collections.Generic;

namespace ApplicationLogic.Repositories.DB
{
    public interface IIncomeAccountDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<IncomeAccount>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<IncomeAccount> GetById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperationResponse<PageResult<IncomeAccountPageQueryCommandOutputDTO>> PageQuery(PageQuery<IncomeAccountPageQueryCommandInputDTO> input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        OperationResponse<DomainModel.IncomeAccount> GetByFullName(string fullName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse Insert(IncomeAccount entity);

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
        OperationResponse Delete(IncomeAccount entity);

        /// <summary>
        /// Logical Delete of an inventory item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OperationResponse LogicalDelete(IncomeAccount entity);

       
    }
}
