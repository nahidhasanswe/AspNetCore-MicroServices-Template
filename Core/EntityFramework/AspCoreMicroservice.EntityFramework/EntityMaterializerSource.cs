﻿using Internal = Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Reflection;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using AspCoreMicroservice.Core.Timing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AspCoreMicroservice.Core.Reflection;

namespace AspCoreMicroservice.Core.EntityFramework
{
    // todo: We can remove this class and use ValueConverters of EF Core when EF 2.1 is released 
    // if ValueConverters can handle DateTime kind normalization properly
    // See https://github.com/aspnet/EntityFrameworkCore/issues/242
 /*   public class EntityMaterializerSource : Internal.EntityMaterializerSource
    {
        private static readonly MethodInfo NormalizeDateTimeMethod =
            typeof(EntityMaterializerSource).GetTypeInfo().GetMethod(nameof(NormalizeDateTime), BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly MethodInfo NormalizeNullableDateTimeMethod =
            typeof(EntityMaterializerSource).GetTypeInfo().GetMethod(nameof(NormalizeNullableDateTime), BindingFlags.Static | BindingFlags.NonPublic);

        public override Expression CreateReadValueExpression(Expression valueBuffer, Type type, int index, IPropertyBase propertyBase)
        {
            if (ShouldDisableDateTimeNormalization(propertyBase))
            {
                return base.CreateReadValueExpression(valueBuffer, type, index, propertyBase);
            }

            if (type == typeof(DateTime))
            {
                return Expression.Call(
                    NormalizeDateTimeMethod,
                    base.CreateReadValueExpression(valueBuffer, type, index, propertyBase)
                );
            }

            if (type == typeof(DateTime?))
            {
                return Expression.Call(
                    NormalizeNullableDateTimeMethod,
                    base.CreateReadValueExpression(valueBuffer, type, index, propertyBase)
                );
            }

            return base.CreateReadValueExpression(valueBuffer, type, index, propertyBase);
        }

        private static DateTime NormalizeDateTime(DateTime value)
        {
            return Clock.Normalize(value);
        }

        private static DateTime? NormalizeNullableDateTime(DateTime? value)
        {
            if (value == null)
            {
                return null;
            }

            return Clock.Normalize(value.Value);
        }

        private static bool ShouldDisableDateTimeNormalization(IPropertyBase propertyBase)
        {
            if (propertyBase == null)
            {
                return false;
            }

            if (propertyBase.PropertyInfo == null)
            {
                return false;
            }

            if (propertyBase.PropertyInfo.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
            {
                return true;
            }

            propertyBase.TryGetMemberInfo(false, false, out var memberInfo, out _);
            return ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(memberInfo) != null;
        }
    }*/
}
