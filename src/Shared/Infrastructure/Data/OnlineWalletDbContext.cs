using Microsoft.EntityFrameworkCore;
using OnlineWallet.Domain;
using OnlineWallet.Domain.Account;
using OnlineWallet.Domain.MobileRecharge;

namespace OnlineWallet.Infrastructure.Data;

public class OnlineWalletDbContext : DbContext
{
    public OnlineWalletDbContext(DbContextOptions<OnlineWalletDbContext> options)
        : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                var now = DateTime.UtcNow;

                switch (entry.State)
                {
                    case EntityState.Added:
                        baseEntity.CreatedAt = now;
                        break;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beneficiary>().Property(p => p.Version).IsRowVersion();
        modelBuilder.Entity<TopUp>().Property(p => p.Version).IsRowVersion();
        modelBuilder.Entity<User>().Property(p => p.Version).IsRowVersion();
            
        modelBuilder.Entity<Account>().ToTable(builder => builder.HasCheckConstraint("CK_NonNegative_Balance","\"Balance\" >= 0::numeric"));
        modelBuilder.Entity<Transaction>().ToTable(builder => builder.HasCheckConstraint("CK_NonNegative_Amount","\"Amount\" >= 0::numeric"));
    }
}