using AspCoreMicroservice.Core.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace AspCoreMicroservice.Core.EntityFramework.Uow
{
    public class EFCoreUnitOfWorkManager<TDbContext> : IUnitOfWorkManager
        where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;

        public EFCoreUnitOfWorkManager(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUnitOfWork Begin()
        {
            return new EfCoreUnitOfWork<TDbContext>(dbContext);
        }
    }
}
