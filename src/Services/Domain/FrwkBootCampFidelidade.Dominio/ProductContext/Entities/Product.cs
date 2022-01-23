using FrwkBootCampFidelidade.Dominio.Base;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;
using System;

namespace FrwkBootCampFidelidade.Dominio.ProductContext.Entities
{
    public class Product : EntityBase
    {
        public string PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
