using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Buriti_Store.Payments.Data.Mappings
{
    public class PaymentMapping : IEntityTypeConfiguration<Payment.Business.Payment>
    {
        public void Configure(EntityTypeBuilder<Payment.Business.Payment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CardName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.CardNumber)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(c => c.CardExpiration)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.CardCvv)
                .IsRequired()
                .HasColumnType("varchar(4)");

            // 1 : 1 => Pagamento : Transacao
            builder.HasOne(c => c.Transaction)
                .WithOne(c => c.Payment);

            builder.ToTable("Payments");
        }
    }
}