// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using Microsoft.EntityFrameworkCore;

namespace AspCoreMicroservice.Core.EntityFramework
{
    public sealed class DefaultDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext DbContext { get; }

        public DefaultDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TDbContext GetDbContext()
        {
            return DbContext;
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
