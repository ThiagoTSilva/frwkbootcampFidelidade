using FrwkBootCampFidelidade.Dominio.Base;

namespace Web.BootCampFidelidade.HttpAggregator.Models.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderDTO OrderDTO { get; set; }
        public string Observation { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

    }
}
