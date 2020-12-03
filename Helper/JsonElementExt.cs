using System.Text.Json;

namespace Shinetech.Common.Helper
{
    public static class JsonElementExt
    {
        public static T ToObject<T>(this JsonElement element)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
        public static T ToObject<T>(this JsonDocument document)
        {
            var json = document.RootElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }

        public static string ToString<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
