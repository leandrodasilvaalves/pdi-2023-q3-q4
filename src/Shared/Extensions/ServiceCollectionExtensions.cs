using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.Repositories;
using Shared.Validations;
using Shared.Validations.Rules;

namespace Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.SectionName));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IEntryRepository, EntryRepository>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddAsyncRules();
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IOwnerValidator, OwnerValidator>();
            services.AddScoped<IEntryValidator, EntryValidator>();
            return services;
        }

        private static IServiceCollection AddAsyncRules(this IServiceCollection services)
        {
            services.AddScoped<IDocumentAlreadyRegisteredForThisBank, DocumentAlreadyRegisteredForThisBank>();
            return services;
        }
    }
}