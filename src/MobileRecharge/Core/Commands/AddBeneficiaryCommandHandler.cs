using MediatR;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Core.Validations;
using MobileRecharge.Core.Validations.Beneficiary;
using OnlineWallet.Domain;
using OnlineWallet.Domain.MobileRecharge;
using OnlineWallet.Infrastructure.Data;

namespace MobileRecharge.Core.Commands;

public class AddBeneficiaryCommandHandler : IRequestHandler<AddBeneficiaryCommand>
{
    private readonly OnlineWalletDbContext _onlineWalletDbContext;
    private readonly IBeneficiaryValidationService _beneficiaryValidationService;

    public AddBeneficiaryCommandHandler(OnlineWalletDbContext onlineWalletDbContext, IBeneficiaryValidationService beneficiaryValidationService)
    {
        _onlineWalletDbContext = onlineWalletDbContext;
        _beneficiaryValidationService = beneficiaryValidationService;
    }

    public async Task Handle(AddBeneficiaryCommand addBeneficiaryCommand, CancellationToken cancellationToken)
    {
        var user = await GetUser(addBeneficiaryCommand.UserId, cancellationToken);

        _beneficiaryValidationService.ValidateAddBeneficiary(user, addBeneficiaryCommand);

        var beneficiary = Beneficiary.Create(addBeneficiaryCommand.UserId, addBeneficiaryCommand.MobileNumber, addBeneficiaryCommand.NickName);

        await _onlineWalletDbContext.AddAsync(beneficiary, cancellationToken);
        await _onlineWalletDbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<User?> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await _onlineWalletDbContext.Set<User>()
            .Include(u => u.Beneficiaries)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        if (user == null)
        {
            throw new UserNotFoundException(id);
        }

        return user;
    }
}