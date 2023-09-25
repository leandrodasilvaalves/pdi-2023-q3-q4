using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace Shared.Broker
{
    public class CustomSerializer<TObject> : ISerializer<TObject> where TObject : class
    {
        private static ISerializer<TObject> _instance;

        public byte[] Serialize(TObject data, SerializationContext context)
        {
            var json = JsonSerializer.Serialize(data);
            return Encoding.ASCII.GetBytes(json);
        }

        public static ISerializer<TObject> Instance()
        {
            if (_instance is null)
            {
                _instance = new CustomSerializer<TObject>();
            }
            return _instance;
        }
    }
}