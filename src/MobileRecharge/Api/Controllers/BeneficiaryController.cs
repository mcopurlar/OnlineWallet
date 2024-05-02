using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobileRecharge.Api.Controllers.Models;
using MobileRecharge.Core.Queries;

namespace MobileRecharge.Api.Controllers;

[Route("api/beneficiaries")]
public class BeneficiaryController : Controller
{
    private readonly IMediator _mediator;

    public BeneficiaryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> AddBeneficiary([FromBody] AddBeneficiaryRequestModel addBeneficiaryRequestModel, CancellationToken cancellationToken)
    {
        await _mediator.Send(addBeneficiaryRequestModel.ToCommand(), cancellationToken);
        return Ok();
    }


    [HttpGet]
    public async Task<IActionResult> GetBeneficiaries(Guid userId, CancellationToken cancellationToken)
    {
        var getBeneficiariesByUserIdQuery = new GetBeneficiariesByUserIdQuery
        {
            UserId = userId
        };

        var getBeneficiariesByUserIdResult = await _mediator.Send(getBeneficiariesByUserIdQuery, cancellationToken);
        return Ok(getBeneficiariesByUserIdResult);
    }
}