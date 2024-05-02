using Microsoft.Extensions.Options;
using MobileRecharge.Core.Commands;
using OnlineWallet.Domain;
using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations.TopUp;

public class MaximumTopUpAmountByBeneficiaryValidator : ITopUpValidator
{
    private readonly ValidationConfiguration _validationConfiguration;

    public MaximumTopUpAmountByBeneficiaryValidator(IOptions<ValidationConfiguration> options)
    {
        _validationConfiguration = options.Value;
    }
    public ValidationResult Validate(User user, TopUpCommand topUpCommand)
    {
        var now = DateTime.Now;
        var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

        var beneficiary = user.Beneficiaries.FirstOrDefault(b => b.Id == topUpCommand.BeneficiaryId);

        if (beneficiary == null)
        {
            throw new BeneficiaryNotFoundException(topUpCommand.BeneficiaryId);
        }

        var sumOfTopUpAmountsPerMonth = beneficiary.TopUps
            .Where(t => t.CreatedAt > firstDayOfMonth)
            .Sum(t => t.Amount);

        var maxLimit = user.Status == UserStatus.NotVerified
            ? _validationConfiguration.MaxTopUpTotalAmountByBeneficiaryForVerifiedUser
            : _validationConfiguration.MaxTopUpTotalAmountByBeneficiaryForNotVerifiedUser;

        if (sumOfTopUpAmountsPerMonth + topUpCommand.RechargeValue > maxLimit)
        {
            return new InvalidResult(ErrorMessages.MonthlyTopUpLimitPerBeneficiaryExceeding(maxLimit));
        }

        return new ValidResult();
    }
}