using MediatR;
using MobileRecharge.Core.Results;

namespace MobileRecharge.Core.Queries;

public class GetTopUpOptionsQueryHandler : IRequestHandler<GetTopUpOptionsQuery, GetTopUpOptionsResult>
{
    public  Task<GetTopUpOptionsResult> Handle(GetTopUpOptionsQuery getTopUpOptionsQuery, CancellationToken cancellationToken)
    {
        return  Task.FromResult(new GetTopUpOptionsResult
        {
            new() { Cost = 5 },
            new() { Cost = 10 },
            new() { Cost = 20 },
            new() { Cost = 30 },
            new() { Cost = 50 },
            new() { Cost = 75 },
            new() { Cost = 100 }
        });
    }
}