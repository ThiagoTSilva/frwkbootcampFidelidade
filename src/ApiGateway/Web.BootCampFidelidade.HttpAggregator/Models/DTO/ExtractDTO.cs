using System;
using Web.BootCampFidelidade.HttpAggregator.Models.Enum;

namespace Web.BootCampFidelidade.HttpAggregator.Models.DTO
{
    public class ExtractDTO
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public EnumTransactionType TransactionType { get; set; }
    }
}
