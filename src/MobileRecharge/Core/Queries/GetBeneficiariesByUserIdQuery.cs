using MediatR;
using MobileRecharge.Core.Results;

namespace MobileRecharge.Core.Queries;

public class GetBeneficiariesByUserIdQuery: IRequest<GetBeneficiariesByUserIdResult>
{
    public Guid UserId { get; set; }
}