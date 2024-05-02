using MobileRecharge.Core.Commands;

namespace MobileRecharge.Api.Controllers.Models;

public class TopUpRequestModel
{
    public Guid UserId { get; set; }
    public Guid BeneficiaryId { get; set; }
    public decimal RechargeValue { get; set; }

    public TopUpCommand ToCommand()
    {
        return new TopUpCommand
        {
            UserId = UserId,
            BeneficiaryId = BeneficiaryId,
            RechargeValue = RechargeValue
        };
    }
}