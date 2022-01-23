using FrwkBootCampFidelidade.Dominio.ExtractContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;


namespace FrwkBootCampFidelidade.Infraestrutura.ExtractContext.Mapping
{
    public class RansomHistoryStatusMap : IEntityTypeConfiguration<RansomHistoryStatus>
    {
        public void Configure(EntityTypeBuilder<RansomHistoryStatus> builder)
        {
            builder.ToTable("RansomHistoryStatus");

           // builder.HasData(ObterDados());
        }

        private static ICollection<RansomHistoryStatus> ObterDados()
        {
            DateTime createdAt = new(2021, 12, 13, 15, 45, 23);

            return new[]{
                new RansomHistoryStatus() {Id = 1, RansomStatusId = 1, RansomId = 1, Date = createdAt, CreatedAt = createdAt, UpdatedAt = createdAt },
                new RansomHistoryStatus() {Id = 2, RansomStatusId = 2, RansomId = 2, Date = createdAt, CreatedAt = createdAt, UpdatedAt = createdAt }
            };
        }
    }
}
