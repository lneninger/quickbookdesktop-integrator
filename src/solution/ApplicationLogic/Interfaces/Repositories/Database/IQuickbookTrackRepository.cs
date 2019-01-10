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
        IEnumerable<QuickbookState> GetState(string ticket, string currentStep, string key);

        void AddState(QuickbookState state);
    }
}
