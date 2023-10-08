using Shared.Validations;
using Shared.Extensions;
using Star.Claims.Validators;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStarValidators(this IServiceCollection services)
    {
        services.AddAsyncRules();
        services.AddScoped<IAccountValidator, AccountValidator>();
        services.AddScoped<IOwnerValidator, OwnerValidator>();
        services.AddScoped<IEntryValidator, EntryValidator>();
        services.AddScoped<IGetAccountAddressingKeysValidator, GetAccountAddressingKeysValidator>();
        services.AddScoped<IRegisterClaimValidator, StarRegisterClaimValidator>();
        return services;
    }
}