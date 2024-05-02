using MediatR;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Core.Results;
using MobileRecharge.Core.Services;
using MobileRecharge.Core.Validations;
using MobileRecharge.Core.Validations.TopUp;
using OnlineWallet.Domain;
using OnlineWallet.Domain.MobileRecharge;
using OnlineWallet.Infrastructure.Data;

namespace MobileRecharge.Core.Commands;

public class TopUpCommandHandler : IRequestHandler<TopUpCommand, TopUpResult>
{
    private readonly OnlineWalletDbContext _onlineWalletDbContext;
    private readonly ITopUpValidationService _topUpValidationService;
    private readonly IDebitAccountService _debitAccountService;

    public TopUpCommandHandler(OnlineWalletDbContext onlineWalletDbContext, ITopUpValidationService topUpValidationService, IDebitAccountService debitAccountService)
    {
        _onlineWalletDbContext = onlineWalletDbContext;
        _topUpValidationService = topUpValidationService;
        _debitAccountService = debitAccountService;
    }

    public async Task<TopUpResult> Handle(TopUpCommand topUpCommand, CancellationToken cancellationToken)
    {
        var user = await GetUser(topUpCommand.UserId, cancellationToken);

        _topUpValidationService.Validate(user, topUpCommand);

        var debitAccountResponse = await _debitAccountService.DebitAccount(user.Id, topUpCommand.RechargeValue);

        if (debitAccountResponse.IsSuccessful)
        {
            var topUp = TopUp.Create(topUpCommand.BeneficiaryId, topUpCommand.RechargeValue);

            await _onlineWalletDbContext.AddAsync(topUp, cancellationToken);
            await _onlineWalletDbContext.SaveChangesAsync(cancellationToken);

            return new SuccessfulTopUpResult();
        }

        return new TopUpResult
        {
            IsSuccessful = false,
            ErrorMessages = debitAccountResponse.ErrorMessages
        };
    }

    private async Task<User?> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await _onlineWalletDbContext.Set<User>()
            .Include(u => u.Beneficiaries)
            .ThenInclude(b => b.TopUps)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(id);
        }

        return user;
    }
}