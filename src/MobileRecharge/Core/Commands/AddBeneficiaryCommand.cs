using MediatR;

namespace MobileRecharge.Core.Commands;

public class AddBeneficiaryCommand : IRequest
{
    public Guid UserId { get; set; }
    public string MobileNumber { get; set; }
    public string NickName { get; set; }
}