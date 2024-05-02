using MediatR;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Core.Results;
using OnlineWallet.Domain.MobileRecharge;
using OnlineWallet.Infrastructure.Data;

namespace MobileRecharge.Core.Queries;

public class GetBeneficiariesByUserIdQueryHandler : IRequestHandler<GetBeneficiariesByUserIdQuery, GetBeneficiariesByUserIdResult>
{
    private readonly OnlineWalletDbContext _onlineWalletDbContext;

    public GetBeneficiariesByUserIdQueryHandler(OnlineWalletDbContext onlineWalletDbContext)
    {
        _onlineWalletDbContext = onlineWalletDbContext;
    }

    public async Task<GetBeneficiariesByUserIdResult> Handle(GetBeneficiariesByUserIdQuery getBeneficiariesByUserIdQuery, CancellationToken cancellationToken)
    {
        var beneficiaries = await _onlineWalletDbContext.Set<Beneficiary>().Where(b => b.UserId == getBeneficiariesByUserIdQuery.UserId).ToListAsync();
        var beneficiariesByUserIdResult = new GetBeneficiariesByUserIdResult();

        beneficiaries.ForEach(beneficiary =>
        {
            beneficiariesByUserIdResult.Add(new BeneficiaryRepresentation
            {
                Id = beneficiary.Id,
                MobileNumber = beneficiary.MobileNumber,
                NickName = beneficiary.NickName,
                Status = beneficiary.Status.ToString()
            });
        });

        return beneficiariesByUserIdResult;
    }
}