using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.Product.InsertCommand.Models;
using Framework.Core.Messages;
using DomainModel.Product;
using DomainModel._Commons.Enums;

namespace ApplicationLogic.Business.Commands.Product.InsertCommand
{
    public class ProductInsertCommand : AbstractDBCommand<DomainModel.Product.AbstractProduct, IProductDBRepository>, IProductInsertCommand
    {
        public ProductInsertCommand(IDbContextScopeFactory dbContextScopeFactory, IProductDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<ProductInsertCommandOutputDTO> Execute(ProductInsertCommandInputDTO input)
        {
            var result = new OperationResponse<ProductInsertCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                AbstractProduct entity = null;

                if (input.ProductTypeId == nameof(ProductTypeEnum.FLW))
                {
                    entity = new DomainModel.Product.FlowerProduct
                    {
                        Name = input.Name,
                    };
                }
                else if (input.ProductTypeId == nameof(ProductTypeEnum.COMP))
                {
                    entity = new DomainModel.Product.CompositionProduct
                    {
                        Name = input.Name,
                    };
                }
                else if (input.ProductTypeId == nameof(ProductTypeEnum.HARD))
                {
                    entity = new DomainModel.Product.HardgoodProduct
                    {
                        Name = input.Name,
                    };
                }

                try
                {
                    var insertResult = this.Repository.Insert(entity);
                    result.AddResponse(insertResult);
                    if (result.IsSucceed)
                    {
                        dbContextScope.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    result.AddError("Error Adding Product", ex);
                }

                if (result.IsSucceed)
                {
                    var getByIdResult = this.Repository.GetById(entity.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new ProductInsertCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            Name = getByIdResult.Bag.Name
                        };
                    }

                }
            }

            return result;
        }
    }
}
