using System;

namespace Web.BootCampFidelidade.HttpAggregator.Models.DTO
{
    public class RansomHistoryStatusDTO
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public WalletDTO Wallet { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
