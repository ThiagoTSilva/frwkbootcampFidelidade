using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Mapping
{
    public class WalletTypeMap : IEntityTypeConfiguration<WalletType>
    {
        public void Configure(EntityTypeBuilder<WalletType> builder)
        {
            builder.ToTable("WalletType");
            builder.HasData(ObterDados());
        }
        private ICollection<WalletType> ObterDados()
        {
            DateTime createdAt = new DateTime(2021, 12, 13, 15, 45, 23);

            return new[]{
                new WalletType() { Id = 1, Name = "Pontos", CreatedAt = createdAt, UpdatedAt = createdAt },
                new WalletType() { Id = 2, Name = "Dinheiro", CreatedAt = createdAt, UpdatedAt =  createdAt }
            };
        }
    }
}
