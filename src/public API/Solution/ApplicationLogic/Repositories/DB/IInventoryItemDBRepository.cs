//using ApplicationLogic.Business.Commands.AppUser.AuthenticateCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.PageQueryCommand.Models;
using DomainModel;
using Framework.Core.Messages;
using Microsoft.AspNetCore.Identity;
//using ApplicationLogic.Business.Commands.AppUser.RegisterCommand.Models;
//using ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models;
using System.Collections.Generic;

namespace ApplicationLogic.Repositories.DB
{
    public interface IInventoryItemDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<AppUserGetAllCommandOutputDTO>> All { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<InventoryItem> GetById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperationResponse<PageResult<InventoryItemPageQueryCommandOutputDTO>> PageQuery(PageQuery<AppUserPageQueryCommandInputDTO> input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        OperationResponse<bool> ExistsByEmail(string email);

        OperationResponse<bool> ExistsByUserName(string userName);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //OperationResponse<AppUserRegisterCommandOutputDTO> Insert(AppUserRegisterCommandInputDTO input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //OperationResponse<AppUserAuthenticateCommandOutputDTO> Authenticate(AppUserAuthenticateCommandInputDTO input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //OperationResponse Update(AppUserUpdateCommandInputDTO input);

        /// <summary>
        /// MARK user as deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<AppUserDeleteCommandOutputDTO> Delete(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        OperationResponse<bool> ExistsByEmailOrUserName(string email, string userName);
    }
}
