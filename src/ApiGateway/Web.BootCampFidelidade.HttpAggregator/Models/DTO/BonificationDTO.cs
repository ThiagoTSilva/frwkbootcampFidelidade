using System;

namespace Web.BootCampFidelidade.HttpAggregator.Models.DTO
{
    public class BonificationDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public string CPF { get; set; }
        public float ScoreQuantity { get; set; }
        public float TotalValue { get; set; }
        public DateTime Date { get; set; }
    }
}
