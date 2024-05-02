namespace OnlineWallet.Domain;

public abstract class BaseEntity
{
    protected BaseEntity() { }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}