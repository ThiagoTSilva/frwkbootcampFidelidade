using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FrwkBootCampFidelidade.Infraestrutura.RansomContext.Mapping
{
    public class RansomStatusMap : IEntityTypeConfiguration<RansomStatus>
    {
        public void Configure(EntityTypeBuilder<RansomStatus> builder)
        {
            builder.ToTable("RansomStatus");

            builder.HasKey(prop => prop.Id);

            builder.Property(x => x.Name);

          //  builder.HasData(ObterDados());
        }

        private static ICollection<RansomStatus> ObterDados()
        {
            DateTime createdAt = new(2021, 12, 13, 15, 45, 23);

            return new[]{
                new RansomStatus() {Id = 1, Name = "Solicitado" },
                new RansomStatus() {Id = 2, Name = "Concluído" }
            };
        }
    }
}
