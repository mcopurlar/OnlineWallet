namespace MobileRecharge.Core.Results;

public class SuccessfulTopUpResult : TopUpResult
{
    public SuccessfulTopUpResult()
    {
        IsSuccessful = true;
    }
}