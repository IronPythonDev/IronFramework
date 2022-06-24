using System.Text.Json;

namespace IronFramework.Core.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source) =>
            JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source))!;
    }
}
