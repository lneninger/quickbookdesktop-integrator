using ApplicationLogic.Interfaces.Repositories.Database;
using DatabaseSchema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseRepositories
{
    public class QuickbookTrackRepository : AbstractDBRepository, IQuickbookTrackRepository
    {
        public QuickbookTrackRepository(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public QuickbookExecution AddExecution()
        {
            try
            {
                var entity = new QuickbookExecution
                {
                    Date = DateTime.UtcNow,
                    StatusId = ExecutionStatusEnum.Executing
                };

                var result = this.DbContext.QuickbookExecutions.Add(entity);
                this.DbContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetExecutionStatus(int id, string status)
        {
            try
            {
                var entity = this.DbContext.QuickbookExecutions.Find(id);
                if (entity != null)
                {
                    entity.StatusId = status;
                    this.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public QuickbookExecution GetExecutionById(int id)
        {
            return this.DbContext.QuickbookExecutions.Find(id);
        }
    }
}
