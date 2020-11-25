using Domain;
using Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CurrencyWalletContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public CurrencyWalletContext(DbContextOptions<CurrencyWalletContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WalletMapping());
            modelBuilder.ApplyConfiguration(new TransactionMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
