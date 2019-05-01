using ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand.Models;
using ApplicationLogic.Repositories.DB;
using EntityFrameworkCore.DbContextScope;
using Framework.Core.Messages;
using System;

namespace ApplicationLogic.Business.Commands.IntegrationProcess.InsertCommand
{
    public class IntegrationProcessInsertCommand : AbstractDBCommand<DomainModel.IntegrationProcess, IIntegrationProcessDBRepository>, IIntegrationProcessInsertCommand
    {
        public IntegrationProcessInsertCommand(IDbContextScopeFactory dbContextScopeFactory, IIntegrationProcessDBRepository repository) : base(dbContextScopeFactory, repository)
        {
        }

        public OperationResponse<IntegrationProcessInsertCommandOutputDTO> Execute(IntegrationProcessInsertCommandInputDTO input)
        {
            var result = new OperationResponse<IntegrationProcessInsertCommandOutputDTO>();
            using (var dbContextScope = this.DbContextScopeFactory.Create())
            {
                var entity = new DomainModel.IntegrationProcess
                {
                };


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
                    result.AddError("Error Adding Integration Process", ex);
                }

                if (result.IsSucceed)
                {
                    var getByIdResult = this.Repository.GetById(entity.Id);
                    result.AddResponse(getByIdResult);
                    if (result.IsSucceed)
                    {
                        result.Bag = new IntegrationProcessInsertCommandOutputDTO
                        {
                            Id = getByIdResult.Bag.Id,
                            CreatedAt = getByIdResult.Bag.CreatedAt,
                            CreatedBy = getByIdResult.Bag.CreatedBy
                        };
                    }

                }
            }

            return result;
        }
    }
}
