using FrwkBootCampFidelidade.Dominio.Base;
using System;

namespace FrwkBootCampFidelidade.Dominio.WalletContext.Entities
{
    public class WalletHistoryTransfer : EntityBase
    {
        public int WalletOriginId { get; set; }
        public int WalletTargetId { get; set; }
        public float Quantity { get; set; }
        public DateTime CreatedAt { get; set; }   
    }
}
