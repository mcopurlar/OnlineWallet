namespace OnlineWallet.Infrastructure.Validations;

public class ErrorMessage
{
    public ErrorMessage(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public string Code { get; set; }
    public string Description { get; set; }
}