using Confluent.Kafka;

namespace Shared.Broker
{
    public interface IPublisher<TMessage> where TMessage : class
    {
        Task PublishAsync(string topicName, TMessage message);
    }

    public class Publisher<TMessage> : IPublisher<TMessage> where TMessage : class
    {
        private readonly IProducer<Null, TMessage> _producer;

        public Publisher(IProducer<Null, TMessage> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(string topicName, TMessage message) =>
            await _producer.ProduceAsync(topicName, new Message<Null, TMessage> { Value = message });
    }
}