using OnlineWallet.Infrastructure.Validations;

namespace AccountBalance.Core.Validations.Account;

public class InsufficientBalanceException : BusinessValidationException
{
    public InsufficientBalanceException() :
        base(new InvalidResult(ErrorMessages.InsufficientBalance))
    {
    }
}