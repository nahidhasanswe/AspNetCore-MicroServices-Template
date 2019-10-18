﻿// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using AspCoreMicroservice.Core.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AspCoreMicroservice.Core
{
    [DebuggerStepThrough]
    public static class Check
    {
        public static T NotNull<T>(T value,  string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);

            return value;
        }

        public static string NotNullOrEmpty(string value, string parameterName)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);

            return value;
        }

        public static string NotNullOrWhiteSpace(string value, string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);

            return value;
        }

        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, string parameterName)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);

            return value;
        }
    }
}
