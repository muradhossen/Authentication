using Application.Services.Accounts;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyContainer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
