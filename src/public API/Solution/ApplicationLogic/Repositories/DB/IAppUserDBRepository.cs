//using ApplicationLogic.Business.Commands.AppUser.AuthenticateCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.AppUser.PageQueryCommand.Models;
//using ApplicationLogic.Business.Commands.AppUser.RegisterCommand.Models;
//using ApplicationLogic.Business.Commands.AppUser.UpdateCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;

namespace ApplicationLogic.Repositories.DB
{
    public interface IAppUserDBRepository : IDBRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        OperationResponse<IEnumerable<AppUserGetAllCommandOutputDTO>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<AppUser> GetById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationResponse<IEnumerable<IdentityRole>> GetRolesByUserId(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperationResponse<PageResult<AppUserPageQueryCommandOutputDTO>> PageQuery(PageQuery<AppUserPageQueryCommandInputDTO> input);

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
