using Microsoft.EntityFrameworkCore;

namespace AspCoreMicroservice.Core.EntityFramework.Configuration
{
    public interface IDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        void Configure(DbContextConfiguration<TDbContext> configuration);
    }
}
