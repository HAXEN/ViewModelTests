using System.IO;
using System.Net.Http;
using Techsson.Gaming.ControlPanel.Core;

namespace CoreTests
{
    public static class MediaTypeFormatterExtensions
    {
        public static string Serialize<T>(this T instance) where T : ViewModelBase
        {
            var stream = new MemoryStream();
            var content = new StreamContent(stream);

            var formatter = JsonFormatterSettingsProvider.CreateJsonFormatter();

            formatter.WriteToStreamAsync(typeof(T), instance, stream, content, null).Wait();

            stream.Position = 0;

            return content.ReadAsStringAsync().Result;
        }
    }
}