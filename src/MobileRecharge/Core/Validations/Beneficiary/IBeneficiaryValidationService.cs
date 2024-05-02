using MobileRecharge.Core.Commands;
using OnlineWallet.Domain;

namespace MobileRecharge.Core.Validations.Beneficiary;

public interface IBeneficiaryValidationService
{
    void ValidateAddBeneficiary(User user, AddBeneficiaryCommand addBeneficiaryCommand);
}