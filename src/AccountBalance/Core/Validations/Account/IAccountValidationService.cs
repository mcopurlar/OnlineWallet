using AccountBalance.Core.Commands;

namespace AccountBalance.Core.Validations.Account;

public interface IAccountValidationService
{
    void ValidateDebitTransaction(OnlineWallet.Domain.Account.Account account, DebitAccountCommand debitAccountCommand);
}