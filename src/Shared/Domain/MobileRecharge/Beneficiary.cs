namespace OnlineWallet.Domain.MobileRecharge;

public class Beneficiary : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string NickName { get; set; }
    public string MobileNumber { get; set; }

    public BeneficiaryStatus Status { get; set; }

    public IList<TopUp> TopUps { get; set; } = new List<TopUp>();
    public uint Version { get; set; }

    public static Beneficiary Create(Guid userId, string mobileNumber, string nickName)
    {
        return new Beneficiary
        {
            UserId = userId,
            MobileNumber = mobileNumber,
            Status = BeneficiaryStatus.Active,
            NickName = nickName
        };
    }
}