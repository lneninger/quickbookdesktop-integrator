using DomainDatabaseMapping;
using DomainModel;
using EntityFrameworkCore.DbContextScope;
using FizzWare.NBuilder;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.PriceLevel.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.InsertCommand.Models;
using ApplicationLogic.Business.Commands.PriceLevel.UpdateCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLogic.Business.Commands.PriceLevel.PageQueryCommand.Models;
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
    public class PriceLevelDBRepository : AbstractDBRepository, IPriceLevelDBRepository
    {
        public PriceLevelDBRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        public OperationResponse<IEnumerable<PriceLevel>> GetAll()
        {
            var result = new OperationResponse<IEnumerable<PriceLevel>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevel>().AsNoTracking().AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all PriceLevel", ex);
            }

            return result;
        }

        public OperationResponse<PageResult<PriceLevelPageQueryCommandOutputDTO>> PageQuery(PageQuery<PriceLevelPageQueryCommandInputDTO> input)
        {
            var result = new OperationResponse<PageResult<PriceLevelPageQueryCommandOutputDTO>>();
            try
            {
                // predicate construction
                var predicate = PredicateBuilderExtension.True<PriceLevel>();
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
                    var query = dbLocator.Set<PriceLevel>().AsQueryable();

                    var advancedSorting = new List<SortItem<PriceLevel>>();
                    Expression<Func<PriceLevel, object>> expression;
                    //if (input.Sort.ContainsKey("productType"))
                    //{
                    //    expression = o => o.ProductType.Name;
                    //    advancedSorting.Add(new SortItem<PriceLevel> { PropertyName = "productType", SortExpression = expression, SortOrder = "desc" });
                    //}

                    var sorting = new SortingDTO<PriceLevel>(input.Sort, advancedSorting);

                    result.Bag = query.ProcessPagingSort<PriceLevel, PriceLevelPageQueryCommandOutputDTO>(predicate, input, sorting, o => new PriceLevelPageQueryCommandOutputDTO
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

        public OperationResponse<DomainModel.PriceLevel> GetById(int id)
        {
            var result = new OperationResponse<DomainModel.PriceLevel>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevel>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Product {id}", ex);
            }

            return result;
        }

        public OperationResponse<DomainModel.PriceLevel> GetByExternalId(string externalId)
        {
            var result = new OperationResponse<DomainModel.PriceLevel>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevel>().Where(o => o.ExternalId == externalId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Price Level {externalId}", ex);
            }

            return result;
        }


        public OperationResponse<PriceLevel> GetByName(string name)
        {
            var result = new OperationResponse<DomainModel.PriceLevel>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<ApplicationDBContext>();
                {
                    result.Bag = dbLocator.Set<PriceLevel>().Where(o => o.Name == name).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Price Level {name}", ex);
            }

            return result;
        }

        public OperationResponse Insert(PriceLevel entity)
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

        public OperationResponse Delete(PriceLevel entity)
        {
            var result = new OperationResponse();

            using (var dbLocator = this.AmbientDbContextLocator.Get<ApplicationDBContext>())
            {
                try
                {
                    dbLocator.Set<PriceLevel>().Remove(entity);
                }
                catch (Exception ex)
                {
                    result.AddException("Error deleting Price Level", ex);
                }
            }

            return null;

        }

        public OperationResponse LogicalDelete(PriceLevel entity)
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
                    result.AddException("Error voiding Price Level", ex);
                }
            }

            return null;
        }

       
    }
}
