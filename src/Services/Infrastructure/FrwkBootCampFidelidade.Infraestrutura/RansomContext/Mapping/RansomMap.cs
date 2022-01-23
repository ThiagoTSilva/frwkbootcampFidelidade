using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrwkBootCampFidelidade.Infraestrutura.RansomContext.Mapping
{
    public class RansomMap : IEntityTypeConfiguration<Ransom>
    {
        public void Configure(EntityTypeBuilder<Ransom> builder)
        {
            builder.ToTable("Ransom");

            builder.HasKey(prop => prop.Id);

            builder.Property(x=>x.WalletId);

            builder.Property(x => x.Amount);

            builder.Property(x => x.Date).HasColumnType("smalldatetime").IsRequired();

            builder.Property(x => x.Beneficiary);

            builder.Property(x => x.CPF);

            builder.Property(x => x.PixKeyType);

            builder.Property(x => x.PixKey);

            builder.Property(x => x.BankNumber);

            builder.Property(x => x.Agency);

            builder.Property(x => x.BankAccountNumber);

            builder.Property(x => x.Operation);

            builder.Property(x => x.Created);

            builder.Property(x => x.Updated);
        }
    }
}
