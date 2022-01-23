
using FrwkBootCampFidelidade.Dominio.Base;
using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using System;

namespace FrwkBootCampFidelidade.Dominio.BonificationContext.Entities
{
    public class Bonification : EntityBase
    {
        public int OrderId { get; set; }        
        public int? UserId { get; set; }
        public string CPF { get; set; }
        public float ScoreQuantity { get; set; }
        public DateTime? ScoreCreditedAt { get; set; }
        public DateTime Date { get; set; }        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
