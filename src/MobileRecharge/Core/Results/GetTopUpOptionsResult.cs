namespace MobileRecharge.Core.Results;

public class GetTopUpOptionsResult : List<TopUpOption>
{

}
public class TopUpOption
{
    public decimal Cost { get; set; }
}