using System.Text.Json;

namespace Shared.Extensions
{
    public static class LogExtensions
    {
        public static void LogJson(this object self, string label = "")
        {
            if(string.IsNullOrEmpty(label))
            {
                label = self.GetType().Name;
            }
            Console.WriteLine("{0}: {1}", label, JsonSerializer.Serialize(self, 
                new JsonSerializerOptions() { WriteIndented = true }));
        }
    }
}