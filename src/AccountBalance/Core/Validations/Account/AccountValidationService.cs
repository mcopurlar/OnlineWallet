using AccountBalance.Core.Commands;

namespace AccountBalance.Core.Validations.Account;

public class AccountValidationService : IAccountValidationService
{
    public void ValidateDebitTransaction(OnlineWallet.Domain.Account.Account account, DebitAccountCommand debitAccountCommand)
    {
        if (account == null)
        {
            throw new AccountNotFoundException(debitAccountCommand.UserId);
        }

        if (account.Balance - debitAccountCommand.Amount < 0)
        {
            throw new InsufficientBalanceException();
        }
    }
}