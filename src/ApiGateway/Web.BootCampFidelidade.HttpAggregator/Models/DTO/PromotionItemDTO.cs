namespace Web.BootCampFidelidade.HttpAggregator.Models.DTO
{
    public class PromotionItemDTO
    {
        public long ProductId { get; set; }
        public double DiscountPercentage { get; set; }
        public ProductDTO Product { get; set; }
    }
}
