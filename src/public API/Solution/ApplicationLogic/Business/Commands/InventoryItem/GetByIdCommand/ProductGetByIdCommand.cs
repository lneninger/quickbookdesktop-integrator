using System;
using EntityFrameworkCore.DbContextScope;
using ApplicationLogic.Repositories.DB;
using ApplicationLogic.Business.Commands.Product.GetByIdCommand.Models;
using Framework.Core.Messages;
using System.Linq;
using ApplicationLogic.Business.Commons.DTOs;
using DomainModel.Product;
using DomainModel._Commons.Enums;

namespace ApplicationLogic.Business.Commands.Product.GetByIdCommand
{
    public class ProductGetByIdCommand : AbstractDBCommand<DomainModel.Product.AbstractProduct, IProductDBRepository>, IProductGetByIdCommand
    {

        public ProductGetByIdCommand(IDbContextScopeFactory dbContextScopeFactory, IProductDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<ProductGetByIdCommandOutputDTO> Execute(int id)
        {
            var result = new OperationResponse<ProductGetByIdCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var getByIdResult = this.Repository.GetByIdWithMedias(id);
                result.AddResponse(getByIdResult);
                if (result.IsSucceed)
                {
                    
                    result.Bag = new ProductGetByIdCommandOutputDTO
                    {
                        Id = getByIdResult.Bag.Id,
                        Name = getByIdResult.Bag.Name,
                        ProductTypeId = getByIdResult.Bag.ProductTypeId,
                        Medias = getByIdResult.Bag.ProductMedias.Select(m => new FileItemRefOutputDTO
                        {
                            Id = m.Id,
                            FileId = m.FileRepositoryId,
                            FullUrl = m.FileRepository.FullFilePath
                        }).ToList(),
                        ProductAllowedColorTypes = getByIdResult.Bag.ProductAllowedColorTypes.Select(m => new ProductGetByIdCommandOutputAllowedColorTypeItemDTO
                        {
                            Id = m.Id,
                            ProductColorTypeId = m.ProductColorTypeId,
                        }).ToList()
                    };

                    if (result.Bag.ProductTypeEnum == ProductTypeEnum.COMP)
                    {
                        result.Bag.RelatedProducts = ((CompositionProduct)getByIdResult.Bag).Items.Select(o => new ProductGetByIdCommandOutputRelatedProductItemDTO
                        {
                            Id = o.Id,
                            ProductId = o.CompositionProductId,
                            RelatedProductId = o.CompositionItemId,
                            Stems = o.Stems,
                            RelatedProductName = o.CompositionItem.Name,
                            RelatedProductTypeName = o.CompositionItem.ProductType.Name,
                            RelatedProductTypeDescription = o.CompositionItem.ProductType.Description,
                            RelatedProductPictureId = o.CompositionItem.ProductMedias.Select(media => media.FileRepositoryId).FirstOrDefault()

                        }).ToList();
                    }
                    else if (result.Bag.ProductTypeEnum == ProductTypeEnum.FLW)
                    {
                        result.Bag.ProductColorTypeId = ((FlowerProduct)getByIdResult.Bag).ProductColorTypeId;

                    }
                }
            }

            return result;



        }
    }
}
