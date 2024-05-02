using MobileRecharge.Core.Commands;
using OnlineWallet.Domain;
using OnlineWallet.Infrastructure.Validations;

namespace MobileRecharge.Core.Validations.TopUp;

public interface ITopUpValidator
{
    ValidationResult Validate(User user, TopUpCommand topUpCommand);
}