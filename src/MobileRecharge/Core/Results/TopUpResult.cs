namespace MobileRecharge.Core.Results;

public class TopUpResult
{
    public bool IsSuccessful { get; set; }

    public IList<string> ErrorMessages { get; set; } = new List<string>();
}