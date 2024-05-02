using MediatR;
using MobileRecharge.Core.Results;

namespace MobileRecharge.Core.Commands;

public class TopUpCommand : IRequest<TopUpResult>
{
    public Guid UserId { get; set; }
    public Guid BeneficiaryId { get; set; }
    public decimal RechargeValue { get; set; }
}