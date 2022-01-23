namespace Web.BootCampFidelidade.HttpAggregator.Models.DTO
{
    public class WalletTransferDTO
    {
        public int WalletOriginId { get; set; }
        public int WalletTargetId { get; set; }
        public float Quantity { get; set; }
    }
}
