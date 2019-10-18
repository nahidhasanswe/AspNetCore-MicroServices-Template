using System;
using System.Threading.Tasks;
using AspCoreMicroservice.Core.Domain.Entities;
using AspCoreMicroservice.Core.Domain.Repositories;

namespace AspCoreMicroservice.Core.Domain.UnitOfWork
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// </summary>
    public abstract class UnitOfWork : IUnitOfWork
    {
        private bool disposed;

        /// <inheritdoc/>
        public abstract void SaveChanges();

        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            disposed = true;         
        }      
    }
}
