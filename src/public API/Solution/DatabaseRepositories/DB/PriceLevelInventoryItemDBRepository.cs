using DomainDatabaseMapping;
using DomainModel;
using EntityFrameworkCore.DbContextScope;
using FizzWare.NBuilder;
using ApplicationLogic.Repositories.DB;
//using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetAllCommand.Models;
//using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.GetByIdCommand.Models;
//using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.InsertCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.UpdateCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLogic.Business.Commands.PriceLevelInventoryItem.PageQueryCommand.Models;
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
    public class PriceLevelInventoryItemDBRepository : AbstractDBRepository, IPriceLevelInventoryItemDBRepository
    {
        public PriceLevelInventoryItemDBRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        public OperationResponse<IEnumerable<PriceLevelInventoryItem>> GetAll()
        {
            var result = new OperationResponse<IEnumerable<PriceLevelInventoryItem>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevelInventoryItem>().AsNoTracking().AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all PriceLevelInventoryItem", ex);
            }

            return result;
        }


        public OperationResponse<IEnumerable<PriceLevelInventoryItem>> GetAllWithPriceLevel()
        {
            var result = new OperationResponse<IEnumerable<PriceLevelInventoryItem>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevelInventoryItem>().Include(t => t.PriceLevel).AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all PriceLevelInventoryItem", ex);
            }

            return result;
        }

        public OperationResponse<PageResult<PriceLevelInventoryItemPageQueryCommandOutputDTO>> PageQuery(PageQuery<PriceLevelInventoryItemPageQueryCommandInputDTO> input)
        {
            var result = new OperationResponse<PageResult<PriceLevelInventoryItemPageQueryCommandOutputDTO>>();
            try
            {
                // predicate construction
                var predicate = PredicateBuilderExtension.True<PriceLevelInventoryItem>();
                if (input.CustomFilter != null)
                {
                    var filter = input.CustomFilter;
                    //if (!string.IsNullOrWhiteSpace(filter.Term))
                    //{
                    //    predicate = predicate.And(o => o.Name.Contains(filter.Term, StringComparison.InvariantCultureIgnoreCase));
                    //}
                }

                var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    var query = dbLocator.Set<PriceLevelInventoryItem>().AsQueryable();

                    var advancedSorting = new List<SortItem<PriceLevelInventoryItem>>();
                    Expression<Func<PriceLevelInventoryItem, object>> expression;
                    //if (input.Sort.ContainsKey("productType"))
                    //{
                    //    expression = o => o.ProductType.Name;
                    //    advancedSorting.Add(new SortItem<PriceLevelInventoryItem> { PropertyName = "productType", SortExpression = expression, SortOrder = "desc" });
                    //}

                    var sorting = new SortingDTO<PriceLevelInventoryItem>(input.Sort, advancedSorting);

                    result.Bag = query.ProcessPagingSort<PriceLevelInventoryItem, PriceLevelInventoryItemPageQueryCommandOutputDTO>(predicate, input, sorting, o => new PriceLevelInventoryItemPageQueryCommandOutputDTO
                    {
                        Id = o.Id,
                    });
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting inventory item page query", ex);
            }

            return result;
        }

        public OperationResponse<DomainModel.PriceLevelInventoryItem> GetById(int id)
        {
            var result = new OperationResponse<DomainModel.PriceLevelInventoryItem>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevelInventoryItem>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Product Level Item{id}", ex);
            }

            return result;
        }

        public OperationResponse<DomainModel.PriceLevelInventoryItem> GetByExternalIdAndPriceLevel(string externalId, string priceLevelExternalId)
        {
            var result = new OperationResponse<DomainModel.PriceLevelInventoryItem>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevelInventoryItem>().Where(o => o.InventoryItem.ExternalId == externalId && o.PriceLevel.ExternalId == priceLevelExternalId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Price Level {externalId}", ex);
            }

            return result;
        }


        public OperationResponse Insert(PriceLevelInventoryItem entity)
        {
            var result = new OperationResponse();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                dbLocator.Add(entity);
            }
            catch (Exception ex)
            {
                result.AddException($"Error adding Price Level", ex);
            }

            return result;
        }

        public OperationResponse Delete(PriceLevelInventoryItem entity)
        {
            var result = new OperationResponse();

            var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>();
            {
                try
                {
                    dbLocator.Set<PriceLevelInventoryItem>().Remove(entity);
                }
                catch (Exception ex)
                {
                    result.AddException("Error deleting Price Level", ex);
                }
            }

            return null;

        }


        //public OperationResponse LogicalDelete(PriceLevelInventoryItem entity)
        //{
        //    var result = new OperationResponse();

        //    using (var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>())
        //    {
        //        try
        //        {
        //            if (!(entity.IsDeleted ?? false))
        //            {
        //                entity.DeletedAt = DateTime.UtcNow;
        //                dbLocator.SaveChanges();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result.AddException("Error voiding Price Level", ex);
        //        }
        //    }

        //    return null;
        //}


    }
}
