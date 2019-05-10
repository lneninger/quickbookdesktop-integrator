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
using ApplicationLogic.Business.Commands.InventoryAccount.PageQueryCommand.Models;

namespace DatabaseRepositories.DB
{
    public class InventoryAccountDBRepository : AbstractDBRepository, IInventoryAccountDBRepository
    {
        public InventoryAccountDBRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        public OperationResponse<IEnumerable<InventoryAccount>> GetAll()
        {
            var result = new OperationResponse<IEnumerable<InventoryAccount>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryAccount>().AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all InventoryAccount", ex);
            }

            return result;
        }

        public OperationResponse<PageResult<InventoryAccountPageQueryCommandOutputDTO>> PageQuery(PageQuery<InventoryAccountPageQueryCommandInputDTO> input)
        {
            var result = new OperationResponse<PageResult<InventoryAccountPageQueryCommandOutputDTO>>();
            try
            {
                // predicate construction
                var predicate = PredicateBuilderExtension.True<InventoryAccount>();
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
                    var query = dbLocator.Set<InventoryAccount>().AsQueryable();

                    var advancedSorting = new List<SortItem<InventoryAccount>>();
                    Expression<Func<InventoryAccount, object>> expression;
                    //if (input.Sort.ContainsKey("InventoryAccountType"))
                    //{
                    //    expression = o => o.InventoryAccountType.Name;
                    //    advancedSorting.Add(new SortItem<InventoryAccount> { PropertyName = "InventoryAccountType", SortExpression = expression, SortOrder = "desc" });
                    //}

                    var sorting = new SortingDTO<InventoryAccount>(input.Sort, advancedSorting);

                    result.Bag = query.ProcessPagingSort<InventoryAccount, InventoryAccountPageQueryCommandOutputDTO>(predicate, input, sorting, o => new InventoryAccountPageQueryCommandOutputDTO
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

        public OperationResponse<InventoryAccount> GetById(int id)
        {
            var result = new OperationResponse<DomainModel.InventoryAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryAccount>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Inventory Account {id}", ex);
            }

            return result;
        }

        public OperationResponse<InventoryAccount> GetByExternalId(string externalId)
        {
            var result = new OperationResponse<DomainModel.InventoryAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryAccount>().Where(o => o.ExternalId == externalId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Inventory Account {externalId}", ex);
            }

            return result;
        }


        public OperationResponse<InventoryAccount> GetInventoryAccountById(int id)
        {
            var result = new OperationResponse<DomainModel.InventoryAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryAccount>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Inventory Account {id}", ex);
            }

            return result;
        }

        public OperationResponse<InventoryAccount> GetByFullName(string fullName)
        {
            var result = new OperationResponse<DomainModel.InventoryAccount>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryAccount>().Where(o => o.FullName == fullName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting InventoryAccount {fullName}", ex);
            }

            return result;
        }

        public OperationResponse<IEnumerable<InventoryAccount>> GetCategories()
        {
            var result = new OperationResponse<IEnumerable<InventoryAccount>>();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
            {
                try
                {
                    result.Bag = dbLocator.Set<InventoryAccount>().ToList();
                }
                catch (Exception ex)
                {
                    result.AddException("Error getting Inventory accounts", ex);
                }
            }

            return null;

        }

        public OperationResponse Insert(InventoryAccount entity)
        {
            var result = new OperationResponse();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                dbLocator.Add(entity);
            }
            catch (Exception ex)
            {
                result.AddException($"Error adding InventoryAccount", ex);
            }

            return result;
        }

        public OperationResponse Delete(InventoryAccount entity)
        {
            var result = new OperationResponse();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
            {
                try
                {
                    dbLocator.Set<InventoryAccount>().Remove(entity);
                }
                catch (Exception ex)
                {
                    result.AddException("Error deleting Inventory Account", ex);
                }
            }

            return null;

        }

        public OperationResponse LogicalDelete(InventoryAccount entity)
        {
            var result = new OperationResponse();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
            {
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
            }

            return null;
        }


    }
}
