namespace OnlineWallet.Infrastructure.Validations;

public class BusinessValidationException : BaseException
{
    public BusinessValidationException(List<ValidationResult> invalidResults)
    {
        InvalidResults = invalidResults.Select(r => new InvalidResult(r.ErrorMessage)).ToList();
    }

    public BusinessValidationException(InvalidResult invalidResult)
    {
        InvalidResults = [invalidResult];
    }
}