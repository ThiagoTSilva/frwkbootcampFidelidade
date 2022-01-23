using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Mapping
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallet");
            //builder.HasData(ObterDados());
        }
        private ICollection<Wallet> ObterDados()
        {
            DateTime createdAt = new DateTime(2021, 12, 13, 15, 45, 23);

            return new[]{
                new Wallet() {Id = 1, UserId = 1, WalletTypeId = 1, DrugstoreId = 1, CreatedAt = createdAt, UpdatedAt = createdAt, Amount = 50.0f },
                new Wallet() {Id = 2, UserId = 1, WalletTypeId = 2, DrugstoreId = 1, CreatedAt = createdAt, UpdatedAt = createdAt, Amount = 150.0f },
            };
        }

    }
}
