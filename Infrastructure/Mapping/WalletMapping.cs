using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class WalletMapping : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(a => a.FirstName).HasMaxLength(150).IsRequired(true);
            builder.Property(a => a.LastName).HasMaxLength(150).IsRequired(true);
            builder.Property(a => a.PhoneNumber).HasMaxLength(150).IsRequired(true);
            builder.Property(a => a.Address).HasMaxLength(150).IsRequired(true);
            builder.Property(a => a.AccountNumber).HasMaxLength(150).IsRequired(true);
            builder.Property(a => a.Balance).IsRequired().HasColumnType("decimal(18, 2)");

            builder.HasIndex(u => u.AccountNumber).IsUnique();
        }
    }
}
