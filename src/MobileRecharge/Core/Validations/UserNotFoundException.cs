using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations;

public class UserNotFoundException : InvalidRequestException
{
    public UserNotFoundException(Guid userId)
        : base(new InvalidResult(ErrorMessages.UserNotFound(userId)))
    {
    }
}