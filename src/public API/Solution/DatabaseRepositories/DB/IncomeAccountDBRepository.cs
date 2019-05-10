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
    public class IncomeAccountDBRepository : AbstractDBRepository, IIncomeAccountDBRepository
    {
        public IncomeAccountDBRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        public OperationResponse<IEnumerable<IncomeAccount>> GetAll()
        {
            var result = new OperationResponse<IEnumerable<IncomeAccount>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IncomeAccount>().AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all IncomeAccount", ex);
            }

            return result;
        }

        public OperationResponse<PageResult<IncomeAccountPageQueryCommandOutputDTO>> PageQuery(PageQuery<IncomeAccountPageQueryCommandInputDTO> input)
        {
            var result = new OperationResponse<PageResult<IncomeAccountPageQueryCommandOutputDTO>>();
            try
            {
                // predicate construction
                var predicate = PredicateBuilderExtension.True<IncomeAccount>();
                if (input.CustomFilter != null)
                {
                    var filter = input.CustomFilter;
                    if (!string.IsNullOrWhiteSpace(filter.Term))
                    {
                        predicate = predicate.And(o => o.Name.Contains(filter.Term, StringComparison.InvariantCultureIgnoreCase));
                    }
                }

                var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    var query = dbLocator.Set<IncomeAccount>().AsQueryable();

                    var advancedSorting = new List<SortItem<IncomeAccount>>();
                    Expression<Func<IncomeAccount, object>> expression;
                    //if (input.Sort.ContainsKey("IncomeAccountType"))
                    //{
                    //    expression = o => o.IncomeAccountType.Name;
                    //    advancedSorting.Add(new SortItem<IncomeAccount> { PropertyName = "IncomeAccountType", SortExpression = expression, SortOrder = "desc" });
                    //}

                    var sorting = new SortingDTO<IncomeAccount>(input.Sort, advancedSorting);

                    result.Bag = query.ProcessPagingSort<IncomeAccount, IncomeAccountPageQueryCommandOutputDTO>(predicate, input, sorting, o => new IncomeAccountPageQueryCommandOutputDTO
                    {
                        Id = o.Id,
                        ExternalId = o.ExternalId,
                        Name = o.Name,
                        CreatedAt = o.CreatedAt,
                    });
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting inventory item page query", ex);
            }

            return result;
        }

        public OperationResponse<IncomeAccount> GetById(int id)
        {
            var result = new OperationResponse<DomainModel.IncomeAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IncomeAccount>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Income Account {id}", ex);
            }

            return result;
        }

        public OperationResponse<IncomeAccount> GetByExternalId(string externalId)
        {
            var result = new OperationResponse<DomainModel.IncomeAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IncomeAccount>().Where(o => o.ExternalId == externalId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Income Account {externalId}", ex);
            }

            return result;
        }


        public OperationResponse<IncomeAccount> GetIncomeAccountById(int id)
        {
            var result = new OperationResponse<DomainModel.IncomeAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IncomeAccount>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Income Account {id}", ex);
            }

            return result;
        }

        public OperationResponse<IncomeAccount> GetByFullName(string fullName)
        {
            var result = new OperationResponse<DomainModel.IncomeAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<IncomeAccount>().Where(o => o.FullName == fullName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting IncomeAccount {fullName}", ex);
            }

            return result;
        }

        public OperationResponse<IEnumerable<IncomeAccount>> GetCategories()
        {
            var result = new OperationResponse<IEnumerable<IncomeAccount>>();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
            {
                try
                {
                    result.Bag = dbLocator.Set<IncomeAccount>().ToList();
                }
                catch (Exception ex)
                {
                    result.AddException("Error getting income accounts", ex);
                }
            }

            return null;

        }

        public OperationResponse Insert(IncomeAccount entity)
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

        public OperationResponse Delete(IncomeAccount entity)
        {
            var result = new OperationResponse();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
                try
                {
                    dbLocator.Set<IncomeAccount>().Remove(entity);
                }
                catch (Exception ex)
                {
                    result.AddException("Error deleting Income Account", ex);
                }

            return null;

        }

        public OperationResponse LogicalDelete(IncomeAccount entity)
        {
            var result = new OperationResponse();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
                try
                {
                    if (!(entity.IsDeleted ?? false))
                    {
                        entity.DeletedAt = DateTime.UtcNow;
                        dbLocator.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    result.AddException("Error voiding Inventory Item", ex);
                }

            return null;
        }

       
    }
}
