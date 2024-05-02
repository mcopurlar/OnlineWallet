namespace OnlineWallet.Domain.Account;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public static Transaction Create(Guid accountId, decimal amount, TransactionType transactionType)
    {
        return new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            Type = transactionType
        };
    }
}