// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using System.Collections.Generic;
using System.Reflection;

namespace AspCoreMicroservice.Core.EntityFramework.Utils
{
    internal class EntityDateTimePropertiesInfo
    {
        public List<PropertyInfo> DateTimePropertyInfos { get; set; }

        public List<string> ComplexTypePropertyPaths { get; set; }

        public EntityDateTimePropertiesInfo()
        {
            DateTimePropertyInfos = new List<PropertyInfo>();
            ComplexTypePropertyPaths = new List<string>();
        }
    }
}
