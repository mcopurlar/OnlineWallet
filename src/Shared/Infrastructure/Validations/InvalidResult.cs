namespace OnlineWallet.Infrastructure.Validations;

public class InvalidResult : ValidationResult
{
    public InvalidResult(ErrorMessage errorMessage)
    {
        ErrorMessage = errorMessage;
        IsValid = false;
    }
}