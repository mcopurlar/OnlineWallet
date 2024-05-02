using AccountBalance.Core.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AccountBalance.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<DebitAccountCommand>, DebitAccountCommandHandler>();

        return services;
    }
}