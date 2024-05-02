using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations;

public class ErrorMessages
{
    public static ErrorMessage MonthlyTopUpLimitExceeding(decimal maxLimit)
    {
        return new ErrorMessage(nameof(MonthlyTopUpLimitPerBeneficiaryExceeding),
            $"Monthly top up limit cannot be more than {maxLimit} AED");
    }

    public static ErrorMessage MonthlyTopUpLimitPerBeneficiaryExceeding(decimal maxLimit)
    {
        return new ErrorMessage(nameof(MonthlyTopUpLimitPerBeneficiaryExceeding),
            $"Monthly top up limit cannot be more than {maxLimit} AED per beneficiary");
    }

    public static ErrorMessage UserNotFound(Guid userId)
    {
        return new ErrorMessage(nameof(UserNotFound),
            $"User with id <{userId}> not found");
    }
    
    public static ErrorMessage BeneficiaryNotFound(Guid id)
    {
        return new ErrorMessage(nameof(BeneficiaryNotFound),
            $"Beneficiary with id <{id}> not found");
    }

    public static ErrorMessage MaxActiveBeneficiaryCountExceeding(int maxLimit)
    {
        return new ErrorMessage(nameof(MaxActiveBeneficiaryCountExceeding),
            $"User can have maximum <{maxLimit}> beneficiaries");
    }
}