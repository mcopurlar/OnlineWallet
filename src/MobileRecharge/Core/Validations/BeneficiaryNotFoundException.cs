using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations;

public class BeneficiaryNotFoundException : InvalidRequestException
{
    public BeneficiaryNotFoundException(Guid beneficiaryId)
        : base(new InvalidResult(ErrorMessages.BeneficiaryNotFound(beneficiaryId)))
    {
    }
}