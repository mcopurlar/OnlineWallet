using MobileRecharge.Core.Commands;
using OnlineWallet.Domain;

namespace MobileRecharge.Core.Validations.TopUp;

public interface ITopUpValidationService
{
    void Validate(User user, TopUpCommand topUpCommand);
}