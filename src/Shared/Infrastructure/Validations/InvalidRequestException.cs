namespace OnlineWallet.Infrastructure.Validations;

public class InvalidRequestException : BaseException
{
    public InvalidRequestException(InvalidResult invalidResult)
    {
        InvalidResults = [invalidResult];
    }
}