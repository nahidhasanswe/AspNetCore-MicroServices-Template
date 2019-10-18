// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using Microsoft.EntityFrameworkCore;

namespace AspCoreMicroservice.Core.EntityFramework
{
    public interface IDbContextProvider<out TDbContext>
         where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
