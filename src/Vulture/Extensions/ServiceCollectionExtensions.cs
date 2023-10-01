using Shared.Validations;
using Shared.Extensions;
using Vulture.Validators;

namespace Vulture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVultureValidators(this IServiceCollection services)
        {
            services.AddAsyncRules();
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IOwnerValidator, OwnerValidator>();
            services.AddScoped<IEntryValidator, EntryValidator>();
            services.AddScoped<IGetAccountAddressingKeysValidator, GetAccountAddressingKeysValidator>();
            services.AddScoped<IRegisterClaimValidator, VultureRegisterClaimValidator>();
            return services;
        }
    }
}