using AspCoreMicroservice.Core.Domain.Entities;
using AspCoreMicroservice.Core.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Domain.UnitOfWork
{
    /// <summary>
    /// Defines a unit of work.
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// </summary>
        Task SaveChangesAsync();
    }
}
