using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FrwkBootCampFidelidade.Infraestrutura.OrderItemContext.Mapping
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasData(ObterDados());
    
        }
        private ICollection<OrderItem> ObterDados()
        {            
            return new[]{
                new OrderItem() { Id = 1, OrderId = 1, Observation = "Medicamento X.1", ProductId = 1, Quantity = 5 },
                new OrderItem() { Id = 2, OrderId = 1, Observation = "Medicamento X.2", ProductId = 2, Quantity = 15 },
                new OrderItem() { Id = 3, OrderId = 1, Observation = "Medicamento X.3", ProductId = 3, Quantity = 25 },
                new OrderItem() { Id = 4, OrderId = 2, Observation = "Medicamento Y.1", ProductId = 4, Quantity = 8 },
                new OrderItem() { Id = 5, OrderId = 2, Observation = "Medicamento Y.2", ProductId = 5, Quantity = 9 },
                new OrderItem() { Id = 6, OrderId = 2, Observation = "Medicamento Y.3", ProductId = 6, Quantity = 10 },
            };
        }
    }
}
