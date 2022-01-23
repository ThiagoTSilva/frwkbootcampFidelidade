using System;

namespace FrwkBootCampFidelidade.DTO.ExtractContext
{
    public class ExtractDTO
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public EnumTransactionType TransactionType { get; set; }
    }
}
