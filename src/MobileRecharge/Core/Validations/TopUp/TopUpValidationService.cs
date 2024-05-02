using MobileRecharge.Core.Commands;
using OnlineWallet.Domain;
using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations.TopUp;

public class TopUpValidationService : ITopUpValidationService
{
    private readonly IList<ITopUpValidator> _topUpValidators;

    public TopUpValidationService(IEnumerable<ITopUpValidator> topUpValidators)
    {
        _topUpValidators = topUpValidators.ToList();
    }

    public void Validate(User user, TopUpCommand topUpCommand)
    {
        var validationResults = _topUpValidators.Select(x => x.Validate(user, topUpCommand));

        var invalidResults = validationResults.Where(vr => !vr.IsValid).ToList();

        if (invalidResults.Any())
        {
            throw new BusinessValidationException(invalidResults);
        }
    }
}