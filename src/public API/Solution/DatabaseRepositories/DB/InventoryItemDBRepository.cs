using DomainDatabaseMapping;
using DomainModel;
using EntityFrameworkCore.DbContextScope;
using FizzWare.NBuilder;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.InventoryItem.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.InsertCommand.Models;
using ApplicationLogic.Business.Commands.InventoryItem.UpdateCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLogic.Business.Commands.InventoryItem.PageQueryCommand.Models;
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
    public class InventoryItemDBRepository : AbstractDBRepository, IInventoryItemDBRepository
    {
        public InventoryItemDBRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        public OperationResponse<IEnumerable<InventoryItem>> GetAll()
        {
            var result = new OperationResponse<IEnumerable<InventoryItem>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryItem>().AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all Product", ex);
            }

            return result;
        }

        public OperationResponse<PageResult<InventoryItemPageQueryCommandOutputDTO>> PageQuery(PageQuery<InventoryItemPageQueryCommandInputDTO> input)
        {
            var result = new OperationResponse<PageResult<InventoryItemPageQueryCommandOutputDTO>>();
            try
            {
                // predicate construction
                var predicate = PredicateBuilderExtension.True<InventoryItem>();
                if (input.CustomFilter != null)
                {
                    var filter = input.CustomFilter;
                    if (!string.IsNullOrWhiteSpace(filter.Term))
                    {
                        predicate = predicate.And(o => o.Name.Contains(filter.Term, StringComparison.InvariantCultureIgnoreCase));
                    }
                }

                using (var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>())
                {
                    var query = dbLocator.Set<InventoryItem>().AsQueryable();

                    var advancedSorting = new List<SortItem<InventoryItem>>();
                    Expression<Func<InventoryItem, object>> expression;
                    //if (input.Sort.ContainsKey("productType"))
                    //{
                    //    expression = o => o.ProductType.Name;
                    //    advancedSorting.Add(new SortItem<InventoryItem> { PropertyName = "productType", SortExpression = expression, SortOrder = "desc" });
                    //}

                    var sorting = new SortingDTO<InventoryItem>(input.Sort, advancedSorting);

                    result.Bag = query.ProcessPagingSort<InventoryItem, InventoryItemPageQueryCommandOutputDTO>(predicate, input, sorting, o => new InventoryItemPageQueryCommandOutputDTO
                    {
                        Id = o.Id,
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

        public OperationResponse<DomainModel.InventoryItem> GetById(int id)
        {
            var result = new OperationResponse<DomainModel.InventoryItem>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryItem>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Product {id}", ex);
            }

            return result;
        }

        public OperationResponse<IncomeAccount> GetIncomeAccountById(string id)
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

        public OperationResponse<InventoryItem> GetByFullName(string fullName)
        {
            var result = new OperationResponse<DomainModel.InventoryItem>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<InventoryItem>().Where(o => o.FullName == fullName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Product {fullName}", ex);
            }

            return result;
        }

        public OperationResponse<IEnumerable<IncomeAccount>> GetCategories()
        {
            var result = new OperationResponse<IEnumerable<IncomeAccount>>();

            using (var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>())
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

        public OperationResponse Insert(InventoryItem entity)
        {
            var result = new OperationResponse();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                dbLocator.Add(entity);
            }
            catch (Exception ex)
            {
                result.AddException($"Error adding Product", ex);
            }

            return result;
        }

        public OperationResponse Delete(InventoryItem entity)
        {
            var result = new OperationResponse();

            using (var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>())
            {
                try
                {
                    dbLocator.Set<InventoryItem>().Remove(entity);
                }
                catch (Exception ex)
                {
                    result.AddException("Error deleting Inventory Item", ex);
                }
            }

            return null;

        }

        public OperationResponse LogicalDelete(InventoryItem entity)
        {
            var result = new OperationResponse();

            using (var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>())
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
