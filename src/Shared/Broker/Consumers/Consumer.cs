using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared.Broker.Consumers
{
    public interface IConsumer<TMessage> where TMessage : class
    {
        Task ConsumeAsync(TMessage message, CancellationToken stoppingToken);
    }

    public abstract class Consumer<TMessage> : BackgroundService, IConsumer<TMessage> where TMessage : class
    {
        private readonly string _topicName;
        private readonly string TName = typeof(TMessage).Name;
        private readonly IConsumer<Null, TMessage> _consumer;

        public Consumer(IServiceProvider provider, string topicName)
        {
            _consumer = provider.GetService<IConsumer<Null, TMessage>>();
            _topicName = topicName;
        }

        public abstract Task ConsumeAsync(TMessage message, CancellationToken stoppingToken);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                Console.WriteLine($"Inicializando consumer...[{_topicName}]");
                try
                {
                    _consumer.Subscribe(_topicName);
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var result = _consumer.Consume(stoppingToken);
                        if (result is null)
                            continue;

                        await ConsumeAsync(result.Message.Value, stoppingToken);
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine($"Encerrando consumer...[{_topicName}]");
                }
                catch (KafkaException ex)
                {
                    Console.WriteLine("Execption: {0}, \nStackTrace: {1}",ex.Message, ex.StackTrace);
                }
                finally
                {
                    _consumer.Close();
                    _consumer.Dispose();
                }
                return Task.CompletedTask;
            });
        }
    }
}