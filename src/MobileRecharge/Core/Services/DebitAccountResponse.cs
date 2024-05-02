namespace MobileRecharge.Core.Services;

public class DebitAccountResponse
{
    public bool IsSuccessful { get; set; }
    public IList<string> ErrorMessages { get; set; } = new List<string>();
}