using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobileRecharge.Api.Controllers.Models;
using MobileRecharge.Core.Queries;

namespace MobileRecharge.Api.Controllers;

[Route("api/mobilerecharge")]
public class MobileRechargeController : Controller
{
    private readonly IMediator _mediator;

    public MobileRechargeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("topupoptions")]
    public async Task<IActionResult> GetTopUpOptions(CancellationToken cancellationToken)
    {
        var topUpOptions = await _mediator.Send(new GetTopUpOptionsQuery(), cancellationToken);
        return Ok(topUpOptions);
    }

    [HttpPost]
    [Route("topup")]
    public async Task<IActionResult> TopUp([FromBody] TopUpRequestModel topUpRequestModel, CancellationToken cancellationToken)
    {
        var topUpResult = await _mediator.Send(topUpRequestModel.ToCommand(), cancellationToken);
        
        if (topUpResult.IsSuccessful)
        {
            return Ok();
        }

        return BadRequest(topUpResult);
    }
}