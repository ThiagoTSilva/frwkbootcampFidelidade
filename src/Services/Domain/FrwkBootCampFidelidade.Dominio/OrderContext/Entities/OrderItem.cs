using FrwkBootCampFidelidade.Dominio.Base;

namespace FrwkBootCampFidelidade.Dominio.OrderContext.Entities
{
    public class OrderItem : EntityBase
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Observation { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

    }
}
