using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MobileRecharge.Core.Commands;
using MobileRecharge.Core.Queries;
using MobileRecharge.Core.Results;
using MobileRecharge.Core.Validations.Beneficiary;
using MobileRecharge.Core.Validations.TopUp;

namespace MobileRecharge.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<AddBeneficiaryCommand>, AddBeneficiaryCommandHandler>();
        services.AddTransient<IRequestHandler<TopUpCommand, TopUpResult>, TopUpCommandHandler>();

        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<GetBeneficiariesByUserIdQuery, GetBeneficiariesByUserIdResult>, GetBeneficiariesByUserIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetTopUpOptionsQuery, GetTopUpOptionsResult>, GetTopUpOptionsQueryHandler>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<ITopUpValidationService, TopUpValidationService>();
        services.AddTransient<IBeneficiaryValidationService, BeneficiaryValidationService>();
        services.AddTransient<ITopUpValidator, MaximumTopUpAmountPerMonthValidator>();
        services.AddTransient<ITopUpValidator, MaximumTopUpAmountByBeneficiaryValidator>();

        return services;
    }
}