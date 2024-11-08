using Application.Notifications;
using Application.Repository;
using Application.Serialization;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureConfiguration
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BankContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IBankAccountAuditRepository, BankAccountAuditRepository>();
        services.AddScoped<INotificationService, AmazonNotificationService>();
        services.AddScoped<ISerialization, SerializationService>();
        services.AddScoped<ITransactionService, TransactionService>();
    }
}