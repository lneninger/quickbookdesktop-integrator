using DatabaseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces.Repositories.Database
{
    public interface IQuickbookTrackRepository: IDbRepository
    {
        QuickbookExecution GetExecutionById(int id);

        QuickbookExecution AddExecution();

        void SetExecutionStatus(int id, string status);
    }
}
