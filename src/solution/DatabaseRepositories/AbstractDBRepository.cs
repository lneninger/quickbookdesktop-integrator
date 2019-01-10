using ApplicationLogic.Interfaces.Repositories.Database;

namespace DatabaseRepositories
{
    public class AbstractDBRepository: IDbRepository
    {
        public AbstractDBRepository(IConnectionFactory connFactory)
        {
            this.DbContext = new AppDbContext(connFactory);
        }

        protected AppDbContext DbContext { get; }

        public void SaveChanges()
        {
            this.DbContext.SaveChanges();
        }
    }
}