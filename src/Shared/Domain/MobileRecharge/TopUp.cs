namespace OnlineWallet.Domain.MobileRecharge;

public class TopUp : BaseEntity
{
    public decimal Amount { get; set; }
    public Guid BeneficiaryId { get; set; }
    public Beneficiary Beneficiary { get; set; } = null!;

    public uint Version { get; set; }

    public static TopUp Create(Guid beneficiaryId, decimal rechargeValue)
    {
        return new TopUp
        {
            Amount = rechargeValue,
            BeneficiaryId = beneficiaryId
        };
    }
}