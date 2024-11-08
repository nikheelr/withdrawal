using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationConfiguration
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IBankAccountService, BankAccountService>();
    }
}