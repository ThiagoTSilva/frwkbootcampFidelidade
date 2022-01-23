using FrwkBootCampFidelidade.Dominio.Base;
using System;

namespace FrwkBootCampFidelidade.Dominio.WalletContext.Entities
{
    public class Wallet : EntityBase
    {
        public int DrugstoreId { get; set; }        
        public int UserId { get; set; }
        public WalletType WalletType { get; set; }
        public int WalletTypeId { get; set; }
        public float Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
