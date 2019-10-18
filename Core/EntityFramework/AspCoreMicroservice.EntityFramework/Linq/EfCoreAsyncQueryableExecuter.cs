// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using AspCoreMicroservice.Core.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.EntityFramework.Linq
{
    public class EfCoreAsyncQueryableExecuter : IAsyncQueryableExecuter
    {
        public Task<int> CountAsync<T>(IQueryable<T> queryable)
        {
            return queryable.CountAsync();
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable)
        {
            return queryable.ToListAsync();
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable)
        {
            return queryable.FirstOrDefaultAsync();
        }
    }
}
