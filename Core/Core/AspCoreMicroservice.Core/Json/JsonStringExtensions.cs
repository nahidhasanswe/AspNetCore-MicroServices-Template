using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspCoreMicroservice.Core.Json
{
    public static class JsonStringExtensions
    {
        public static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions()
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        static JsonStringExtensions()
        {
            DefaultOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public static string ToJsonString(this object obj, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(obj, options ?? DefaultOptions);
        }

        public static object FromJsonString(this string value, Type returnType, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize(value, returnType, options ?? DefaultOptions);
        }

        public static T FromJsonString<T>(this string value, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<T>(value, options ?? DefaultOptions);
        }
    }
}
