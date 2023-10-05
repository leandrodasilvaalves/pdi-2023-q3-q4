using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Broker;
using Shared.Broker.Consumers;

namespace Shared.Extensions
{
    public static class KafkaExtensions
    {
        private static ProducerConfig ProducerConfig { get; set; }
        private static ConsumerConfig ConsumerConfig { get; set; }

        public static IServiceCollection ConfigureKafka(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            ProducerConfig = configuration.GetSection($"{sectionName}:Configuration").Get<ProducerConfig>();
            ConsumerConfig = configuration.GetSection($"{sectionName}:Configuration").Get<ConsumerConfig>();
            services.Configure<KafkaTopcis>(configuration.GetSection($"{sectionName}:Topics"));
            return services;
        }

        public static IServiceCollection AddPublishers<TMessage>(this IServiceCollection services)
            where TMessage : class
        {
            services.AddSingleton<IProducer<Null, TMessage>>(new ProducerBuilder<Null, TMessage>(ProducerConfig)
                .SetValueSerializer(CustomSerializer<TMessage>.Instance())
                .Build());

            services.AddSingleton<IPublisher<TMessage>, Publisher<TMessage>>();
            return services;
        }

        public static IServiceCollection AddConsumer<TConsumer, TMessage>(this IServiceCollection services)
            where TMessage : class
            where TConsumer : class, IConsumer<TMessage>, IHostedService
        {
            ConsumerConfig.EnableAutoCommit = true;
            services.AddSingleton<IConsumer<Null, TMessage>>(new ConsumerBuilder<Null, TMessage>(ConsumerConfig)
                .SetValueDeserializer(CustomDeserializer<TMessage>.Instance())
                .Build());

            services.AddHostedService<TConsumer>();
            return services;
        }
    }
}