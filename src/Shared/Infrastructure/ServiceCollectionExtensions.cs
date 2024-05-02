using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineWallet.Infrastructure.Data;

namespace OnlineWallet.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OnlineWalletDbContext>(builder =>
        {
            builder.UseNpgsql(configuration.GetConnectionString("PostgreSql"));
        });


        return services;
    }
}