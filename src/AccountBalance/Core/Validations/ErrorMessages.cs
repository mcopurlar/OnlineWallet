using OnlineWallet.Infrastructure.Validations;

namespace AccountBalance.Core.Validations;

public class ErrorMessages
{
    public static ErrorMessage InsufficientBalance = new(nameof(InsufficientBalance), "Account does not have sufficient balance");

    public static ErrorMessage AccountForUserNotFound(Guid userId)
    {
        return new ErrorMessage(nameof(AccountForUserNotFound), $"Account for user <{userId}> not found");
    }
}