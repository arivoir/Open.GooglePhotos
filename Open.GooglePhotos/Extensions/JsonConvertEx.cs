using System.Diagnostics;
using System.Threading.Tasks;

namespace Open.GooglePhotos
{
    internal static class JsonConvertEx
    {
        public static string SerializeJson<T>(this T instance, bool escapeNonAscii = false)
        {
            var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            if (escapeNonAscii)
                serializerSettings.StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii;
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(instance, serializerSettings);
            if (escapeNonAscii)
                result = result.Replace("\x7f", "\\u007f");
            return result;
        }

        public static async Task SerializeJsonIntoStreamAsync<T>(this T instance, System.IO.Stream stream)
        {
            Debug.Assert(stream.CanWrite);
            var body = System.Text.UTF8Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(instance));
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            await stream.WriteAsync(body, 0, body.Length);
            stream.SetLength(body.Length);
            await stream.FlushAsync();
        }

        public static T DeserializeJson<T>(this string serializedObject)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedObject);
        }

        public static T DeserializeJson<T>(this System.IO.Stream serializedStream)
        {
            var sw = new System.IO.StreamReader(serializedStream);
            var serializedObject = sw.ReadToEnd();
            sw.Dispose();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedObject);
        }
    }
}
