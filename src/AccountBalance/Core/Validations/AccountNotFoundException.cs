using OnlineWallet.Infrastructure.Validations;

namespace AccountBalance.Core.Validations;

public class AccountNotFoundException : InvalidRequestException
{
    public AccountNotFoundException(Guid userId)
        : base(new InvalidResult(ErrorMessages.AccountForUserNotFound(userId)))
    {

    }
}