using MediatR;

namespace AccountBalance.Core.Commands;

public class DebitAccountCommand : IRequest
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
}