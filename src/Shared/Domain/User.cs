using OnlineWallet.Domain.MobileRecharge;

namespace OnlineWallet.Domain;

public class User : BaseEntity
{
    public string Name { get; set; }
    public UserStatus Status { get; set; }
    public IList<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
    public Account.Account Account { get; set; }

    public uint Version { get; set; }
}