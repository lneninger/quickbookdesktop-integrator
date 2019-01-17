using DomainDatabaseMapping;
using DomainModel;
using EntityFrameworkCore.DbContextScope;
using FizzWare.NBuilder;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.Product.DeleteCommand.Models;
using ApplicationLogic.Business.Commands.Product.GetAllCommand.Models;
using ApplicationLogic.Business.Commands.Product.GetByIdCommand.Models;
using ApplicationLogic.Business.Commands.Product.InsertCommand.Models;
using ApplicationLogic.Business.Commands.Product.UpdateCommand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLogic.Business.Commands.Product.PageQueryCommand.Models;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using LMB.PredicateBuilderExtension;
using Framework.EF.DbContextImpl.Persistance;
using Framework.EF.DbContextImpl.Persistance.Models.Sorting;
using System.Linq.Expressions;
using Framework.Core.Messages;
using DomainModel.Product;
using ApplicationLogic.Business.Commons.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using DomainModel._Commons.Enums;

namespace DatabaseRepositories.DB
{
    public class InventoryItemDBRepository : AbstractDBRepository, IProductDBRepository
    {
        public InventoryItemDBRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }

        public OperationResponse<IEnumerable<AbstractProduct>> GetAll()
        {
            var result = new OperationResponse<IEnumerable<AbstractProduct>>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<RiverdaleDBContext>();
                {
                    result.Bag = dbLocator.Set<AbstractProduct>().AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting all Product", ex);
            }

            return result;
        }

        public OperationResponse<PageResult<ProductPageQueryCommandOutputDTO>> PageQuery(PageQuery<ProductPageQueryCommandInputDTO> input)
        {
            var result = new OperationResponse<PageResult<ProductPageQueryCommandOutputDTO>>();
            try
            {
                // predicate construction
                var predicate = PredicateBuilderExtension.True<AbstractProduct>();
                if (input.CustomFilter != null)
                {
                    var filter = input.CustomFilter;
                    if (!string.IsNullOrWhiteSpace(filter.Term))
                    {
                        predicate = predicate.And(o => o.Name.Contains(filter.Term, StringComparison.InvariantCultureIgnoreCase));
                    }
                }

                using (var dbLocator = this.AmbientDbContextLocator.Get<RiverdaleDBContext>())
                {
                    var query = dbLocator.Set<AbstractProduct>().AsQueryable();

                    var advancedSorting = new List<SortItem<AbstractProduct>>();
                    Expression<Func<AbstractProduct, object>> expression;
                    if (input.Sort.ContainsKey("productType"))
                    {
                        expression = o => o.ProductType.Name;
                        advancedSorting.Add(new SortItem<AbstractProduct> { PropertyName = "productType", SortExpression = expression, SortOrder = "desc" });
                    }

                    var sorting = new SortingDTO<AbstractProduct>(input.Sort, advancedSorting);

                    result.Bag = query.ProcessPagingSort<AbstractProduct, ProductPageQueryCommandOutputDTO>(predicate, input, sorting, o => new ProductPageQueryCommandOutputDTO
                    {
                        Id = o.Id,
                        Name = o.Name,
                        ColorName = o.ProductTypeId == "FLW" && ((FlowerProduct)o).ProductColorType != null ? ((FlowerProduct)o).ProductColorType.Name : null,
                        CreatedAt = o.CreatedAt,
                        MainPicture = o.ProductMedias.Select(m => new FileItemRefOutputDTO
                        {
                            Id = m.Id,
                            FullUrl = m.FileRepository.FullFilePath
                        }).FirstOrDefault()
                    });
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting customer page query", ex);
            }

            return result;
        }

        public OperationResponse<DomainModel.Product.AbstractProduct> GetById(int id)
        {
            var result = new OperationResponse<DomainModel.Product.AbstractProduct>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<RiverdaleDBContext>();
                {
                    result.Bag = dbLocator.Set<AbstractProduct>().Where(o => o.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Product {id}", ex);
            }

            return result;
        }

        public OperationResponse<DomainModel.Product.AbstractProduct> GetByIdWithMedias(int id)
        {
            var result = new OperationResponse<DomainModel.Product.AbstractProduct>();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<RiverdaleDBContext>();
                result.Bag = dbLocator.Set<AbstractProduct>().Include(t => t.ProductMedias).Where(o => o.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.AddException($"Error getting Product {id}", ex);
            }

            return result;
        }



        public OperationResponse Insert(AbstractProduct entity)
        {
            var result = new OperationResponse();
            try
            {
                var dbLocator = AmbientDbContextLocator.Get<RiverdaleDBContext>();
                dbLocator.Add(entity);
            }
            catch (Exception ex)
            {
                result.AddException($"Error adding Product", ex);
            }

            return result;
        }

        //public OperationResponse<ProductUpdateCommandOutputDTO> Update(ProductUpdateCommandInputDTO input)
        //{
        //    var result = new OperationResponse<ProductUpdateCommandOutputDTO>();
        //    var dbLocator = AmbientDbContextLocator.Get<RiverdaleDBContext>();
        //    {
        //        var entity = dbLocator.Set<AbstractProduct>().FirstOrDefault(o => o.Id == input.Id);
        //        if (entity != null)
        //        {
        //            entity.Name = input.Name;
        //        }

        //        dbLocator.SaveChanges();


        //        var dbResult = dbLocator.Set<AbstractProduct>().Where(o => o.Id == entity.Id).Select(o => new ProductUpdateCommandOutputDTO
        //        {
        //            Id = o.Id,
        //            Name = o.Name
        //        }).FirstOrDefault();

        //        result.Bag = dbResult;
        //        return result;
        //    }
        //}

        public OperationResponse Delete(DomainModel.Product.AbstractProduct entity)
        {
            var result = new OperationResponse();

            var dbLocator = this.AmbientDbContextLocator.Get<RiverdaleDBContext>();
            try
            {
                dbLocator.Set<AbstractProduct>().Remove(entity);
            }
            catch (Exception ex)
            {
                result.AddException("Error deleting Product", ex);
            }

            return null;
        }

        public OperationResponse LogicalDelete(AbstractProduct entity)
        {
            var result = new OperationResponse();

            using (var dbLocator = this.AmbientDbContextLocator.Get<RiverdaleDBContext>())
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
                    result.AddException("Error voiding Product Color Type", ex);
                }
            }

            return null;
        }

    }
}
