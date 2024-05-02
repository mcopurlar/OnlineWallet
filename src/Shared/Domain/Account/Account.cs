namespace OnlineWallet.Domain.Account;

public class Account : BaseEntity
{
    public decimal Balance { get; set; }
    public IList<Transaction> Transactions { get; set; } = new List<Transaction>();

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public void AddDebitTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
        Balance -= transaction.Amount;
    }
}