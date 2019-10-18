// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using System;
using System.Data;

namespace AspCoreMicroservice.Core.Transactions.Extensions
{
    public static class IsolationLevelExtensions
    {
        /// <summary>
        /// Converts <see cref="System.Transactions.IsolationLevel"/> to <see cref="IsolationLevel"/>.
        /// </summary>
        public static IsolationLevel ToSystemDataIsolationLevel(this System.Transactions.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case System.Transactions.IsolationLevel.Chaos:
                    return IsolationLevel.Chaos;
                case System.Transactions.IsolationLevel.ReadCommitted:
                    return IsolationLevel.ReadCommitted;
                case System.Transactions.IsolationLevel.ReadUncommitted:
                    return IsolationLevel.ReadUncommitted;
                case System.Transactions.IsolationLevel.RepeatableRead:
                    return IsolationLevel.RepeatableRead;
                case System.Transactions.IsolationLevel.Serializable:
                    return IsolationLevel.Serializable;
                case System.Transactions.IsolationLevel.Snapshot:
                    return IsolationLevel.Snapshot;
                case System.Transactions.IsolationLevel.Unspecified:
                    return IsolationLevel.Unspecified;
                default:
                    throw new Exception("Unknown isolation level: " + isolationLevel);
            }
        }
    }
}
