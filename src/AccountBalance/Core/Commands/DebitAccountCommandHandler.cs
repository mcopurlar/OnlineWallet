using System.Data;
using AccountBalance.Core.Validations.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineWallet.Domain.Account;
using OnlineWallet.Infrastructure.Data;
using OnlineWallet.Infrastructure.Validations;

namespace AccountBalance.Core.Commands;

public class DebitAccountCommandHandler : IRequestHandler<DebitAccountCommand>
{
    private readonly OnlineWalletDbContext _onlineWalletDbContext;
    private readonly IAccountValidationService _accountValidationService;

    public DebitAccountCommandHandler(OnlineWalletDbContext onlineWalletDbContext, IAccountValidationService accountValidationService)
    {
        _onlineWalletDbContext = onlineWalletDbContext;
        _accountValidationService = accountValidationService;
    }

    public async Task Handle(DebitAccountCommand debitAccountCommand, CancellationToken cancellationToken)
    {
        using (var dbTransaction = await _onlineWalletDbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable))
        {
            try
            {
                var account = await _onlineWalletDbContext.Set<Account>().FirstOrDefaultAsync(a => a.UserId == debitAccountCommand.UserId);

                _accountValidationService.ValidateDebitTransaction(account, debitAccountCommand);

                var transaction = Transaction.Create(account.Id, debitAccountCommand.Amount, TransactionType.Debit);

                account.AddDebitTransaction(transaction);

                _onlineWalletDbContext.Set<Account>().Update(account);
                await _onlineWalletDbContext.SaveChangesAsync();

                await dbTransaction.CommitAsync();
            }
            catch (Exception exception)
            {
                if (exception is BaseException)
                {
                    throw;
                }

                await dbTransaction.RollbackAsync();
                throw;
            }
        }
    }
}