// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)

namespace AspCoreMicroservice.Core.Timing
{
    public static class ClockProviders
    {
        public static UnspecifiedClockProvider Unspecified { get; } = new UnspecifiedClockProvider();

        public static LocalClockProvider Local { get; } = new LocalClockProvider();

        public static UtcClockProvider Utc { get; } = new UtcClockProvider();
    }
}
