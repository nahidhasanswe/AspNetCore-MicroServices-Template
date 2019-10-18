using AspCoreMicroservice.Core.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.EntityFramework.Uow
{
    /// <summary>
    /// Implements Unit of work for Entity Framework.
    /// </summary>
    public class EfCoreUnitOfWork<TDbContext> : UnitOfWork
        where TDbContext : DbContext
    {
        private bool disposed = false;
        private readonly TDbContext dbContext;
        
        
        /// <summary>
        /// Creates a new <see cref="EfCoreUnitOfWork"/>.
        /// </summary>
        public EfCoreUnitOfWork(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public override void SaveChanges()
        {
            dbContext.SaveChanges();            
        }

        public override async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            disposed = true;

            base.Dispose(disposing);
        }
    }
}
