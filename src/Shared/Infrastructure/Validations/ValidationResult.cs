namespace OnlineWallet.Infrastructure.Validations;

public abstract class ValidationResult
{
    public bool IsValid { get; set; }

    public ErrorMessage ErrorMessage { get; set; }
}