using Microsoft.Extensions.Options;
using MobileRecharge.Core.Commands;
using OnlineWallet.Domain;
using OnlineWallet.Domain.MobileRecharge;
using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations.Beneficiary;

public class BeneficiaryValidationService : IBeneficiaryValidationService
{
    private readonly ValidationConfiguration _validationConfiguration;

    public BeneficiaryValidationService(IOptions<ValidationConfiguration> options)
    {
        _validationConfiguration = options.Value;
    }

    public void ValidateAddBeneficiary(User user, AddBeneficiaryCommand addBeneficiaryCommand)
    {
        var activeBeneficiaryCount = user.Beneficiaries.Count(b => b.Status == BeneficiaryStatus.Active);

        if (activeBeneficiaryCount + 1 >= _validationConfiguration.MaxActiveBeneficiaryCount)
        {
            throw new BusinessValidationException(new InvalidResult(ErrorMessages.MaxActiveBeneficiaryCountExceeding(_validationConfiguration.MaxActiveBeneficiaryCount)));
        }
    }
}