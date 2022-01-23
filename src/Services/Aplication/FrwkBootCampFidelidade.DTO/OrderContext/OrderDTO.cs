using System.Collections.Generic;

namespace FrwkBootCampFidelidade.DTO.OrderContext
{
    public class OrderDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }
        public double TotalValue { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public string CPF { get; set; }
    }
}
