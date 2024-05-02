using Microsoft.Extensions.Options;
using MobileRecharge.Core.Commands;
using OnlineWallet.Domain;
using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations.TopUp;

public class MaximumTopUpAmountPerMonthValidator : ITopUpValidator
{
    private readonly ValidationConfiguration _validationConfiguration;

    public MaximumTopUpAmountPerMonthValidator(IOptions<ValidationConfiguration> options)
    {
        _validationConfiguration = options.Value;
    }

    public ValidationResult Validate(User user, TopUpCommand topUpCommand)
    {
        var now = DateTime.Now;
        var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

        var sumOfTopUpAmountsPerMonth = user.Beneficiaries
            .SelectMany(x => x.TopUps)
            .Where(t => t.CreatedAt > firstDayOfMonth)
            .Sum(b => b.Amount);

        var maxLimit = _validationConfiguration.MaxTopUpTotalAmountPerMonth;

        if (sumOfTopUpAmountsPerMonth + topUpCommand.RechargeValue > maxLimit)
        {
            return new InvalidResult(ErrorMessages.MonthlyTopUpLimitExceeding(maxLimit));
        }

        return new ValidResult();
    }
}