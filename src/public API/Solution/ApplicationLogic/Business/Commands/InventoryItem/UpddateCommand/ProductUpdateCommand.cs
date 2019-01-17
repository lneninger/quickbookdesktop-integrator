using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.Product.UpdateCommand.Models;
using Framework.Core.Messages;
using DomainModel.Product;

namespace ApplicationLogic.Business.Commands.Product.UpdateCommand
{
    public class ProductUpdateCommand : AbstractDBCommand<DomainModel.Product.AbstractProduct, IProductDBRepository>, IProductUpdateCommand
    {
        public ProductUpdateCommand(IDbContextScopeFactory dbContextScopeFactory, IProductDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<ProductUpdateCommandOutputDTO> Execute(ProductUpdateCommandInputDTO input)
        {
            var result = new OperationResponse<ProductUpdateCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetById(input.Id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    getByIdResult.Bag.Name = input.Name;
                    if (getByIdResult.Bag is FlowerProduct)
                    {
                        (getByIdResult.Bag as FlowerProduct).ProductColorTypeId = input.ProductColorTypeId;
                    }

                    try
                    {
                        dbContextScope.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.AddError("Error updating Product Color Type", ex);
                    }

                    getByIdResult = this.Repository.GetById(input.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new ProductUpdateCommandOutputDTO
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
