using DomainDatabaseMapping;
using DomainModel;
using EntityFrameworkCore.DbContextScope;
using FizzWare.NBuilder;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.IncomeAccount.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.IncomeAccount.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.IncomeAccount.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.IncomeAccount.InsertCommand.Models;
using ApplicationLogic.Business.Commands.IncomeAccount.UpdateCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLogic.Business.Commands.IncomeAccount.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using LMB.PredicateBuilderExtension;
using Framework.EF.DbContextImpl.Persistance;
using Framework.EF.DbContextImpl.Persistance.Models.Sorting;
using System.Linq.Expressions;
using Framework.Core.Messages;
using ApplicationLogic.Business.Commons.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRepositories.DB
{
    public class IntegrationProcessDBRepository : AbstractDBRepository, IIntegrationProcessDBRepository
    {
        public IntegrationProcessDBRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        public OperationResponse<IEnumerable<IntegrationProcess>> GetAll()
        {
            var result = new OperationResponse<IEnumerable<IntegrationProcess>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IntegrationProcess>().AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all Integration Processes", ex);
            }

            return result;
        }


        public OperationResponse<IntegrationProcess> GetById(int id)
        {
            var result = new OperationResponse<DomainModel.IntegrationProcess>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IntegrationProcess>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Integration Process {id}", ex);
            }

            return result;
        }


        public OperationResponse<IntegrationProcess> GetIncomeAccountById(int id)
        {
            var result = new OperationResponse<DomainModel.IntegrationProcess>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IntegrationProcess>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Integration Process {id}", ex);
            }

            return result;
        }


        public OperationResponse<IEnumerable<IntegrationProcess>> GetCategories()
        {
            var result = new OperationResponse<IEnumerable<IntegrationProcess>>();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
            {
                try
                {
                    result.Bag = dbLocator.Set<IntegrationProcess>().ToList();
                }
                catch (Exception ex)
                {
                    result.AddException("Error getting income accounts", ex);
                }
            }

            return null;

        }

        public OperationResponse Insert(IntegrationProcess entity)
        {
            var result = new OperationResponse();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                dbLocator.Add(entity);
            }
            catch (Exception ex)
            {
                result.AddException($"Error adding IncomeAccount", ex);
            }

            return result;
        }

        public OperationResponse Delete(IntegrationProcess entity)
        {
            var result = new OperationResponse();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
            {
                try
                {
                    dbLocator.Set<IntegrationProcess>().Remove(entity);
                }
                catch (Exception ex)
                {
                    result.AddException("Error deleting Income Account", ex);
                }
            }

            return null;

        }

       
    }
}
