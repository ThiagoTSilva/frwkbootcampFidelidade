using FrwkBootCampFidelidade.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.OrderContext.Entities
{
    public class Order : EntityBase
    {
        public List<OrderItem> OrderItems { get; set; }
        public double TotalValue { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }        
        public string CPF { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
