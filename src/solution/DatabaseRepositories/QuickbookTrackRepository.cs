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

        public void AddState(QuickbookState state)
        {
            this.DbContext.States.Add(state);
        }

        public IEnumerable<QuickbookState> GetState(string ticket, string currentStep, string key)
        {
            return this.DbContext.States
                    .Where(m => ticket == null || m.Ticket == ticket)
                    .Where(m => currentStep == null || m.CurrentStep == currentStep)
                    .Where(m => key == null || m.Key == key)
                    .ToList();
        }
    }
}
