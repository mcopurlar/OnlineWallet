using AccountBalance.Api.Controllers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountBalance.Api.Controllers;

[Route("api/accounts")]
public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("debit")]
    [HttpPost]
    public async Task<IActionResult> Charge([FromBody] DebitAccountRequestModel debitAccountRequestModel, CancellationToken cancellationToken)
    {
        await _mediator.Send(debitAccountRequestModel.ToCommand(), cancellationToken);
        return Ok();
    }
}