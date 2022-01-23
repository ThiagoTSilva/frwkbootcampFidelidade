using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FrwkBootCampFidelidade.Infraestrutura.OrderItemContext.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasData(ObterDados());
        }
        private ICollection<Order> ObterDados()
        {
            DateTime createdAt = new DateTime(2021, 12, 13, 15, 45, 23);

            return new[]{
                new Order() {Id = 1, CPF = "02563215479", StoreId = 1, TotalValue = 658.96f, CreatedAt = createdAt, UpdatedAt = createdAt, UserId = 1 },
                new Order() {Id = 2, CPF = "65989847894", StoreId = 1, TotalValue = 1698.66f, CreatedAt = createdAt, UpdatedAt = createdAt, UserId = 1 },
            };
        }

    }
}
