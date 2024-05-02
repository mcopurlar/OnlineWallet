namespace OnlineWallet.Infrastructure.Validations;

public class BaseException : Exception
{
    public List<InvalidResult> InvalidResults { get; set; }

}