using AccountBalance.Core.Commands;

namespace AccountBalance.Api.Controllers.Models;

public class DebitAccountRequestModel
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }

    public DebitAccountCommand ToCommand()
    {
        return new DebitAccountCommand
        {
            UserId = UserId,
            Amount = Amount
        };
    }
}