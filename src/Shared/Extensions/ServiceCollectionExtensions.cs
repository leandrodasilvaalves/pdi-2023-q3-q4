using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.HttpClients;
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
            services.AddSingleton<IClaimRepository, ClaimRepository>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddAsyncRules();
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IOwnerValidator, OwnerValidator>();
            services.AddScoped<IEntryValidator, EntryValidator>();
            services.AddScoped<IGetAccountAddressingKeysValidator, GetAccountAddressingKeysValidator>();
            services.AddScoped<IRegisterClaimValidator, RegisterClaimValidator>();
            return services;
        }

        public static IServiceCollection AddAsyncRules(this IServiceCollection services)
        {
            services.AddScoped<IDocumentAlreadyRegisteredForThisBank, DocumentAlreadyRegisteredForThisBank>();
            services.AddScoped<IAccountMustBeExistsWithValidStatus, AccountMustBeExistsWithValidStatus>();
            services.AddScoped<IAddressingKeyMustBeUniqueRule, AddressingKeyMustBeUniqueRule>();
            services.AddScoped<IAddressingKeyMustBeExists, AddressingKeyMustBeExists>();
            services.AddScoped<IAddressingKeyAlreadyHasAnOpenClaim, AddressingKeyAlreadyHasAnOpenClaim>();
            return services;
        }

        public static IServiceCollection AddBacenHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var bacenUrlBase = configuration.GetSection("Bacen:Url").Get<string>();
            services.AddRefitClient<IBacenAccountClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(bacenUrlBase));
            services.AddRefitClient<IBacenEntryClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(bacenUrlBase));
            services.AddRefitClient<IBacenClaimClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(bacenUrlBase));
            return services;
        }
    }
}