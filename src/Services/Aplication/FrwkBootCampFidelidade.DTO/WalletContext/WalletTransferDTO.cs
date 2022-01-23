using System;
using System.Collections.Generic;
using System.Text;

namespace FrwkBootCampFidelidade.DTO.WalletContext
{
    public class WalletTransferDTO
    {
        public int WalletOriginId { get; set; }
        public int WalletTargetId { get; set; }
        public float Quantity { get; set; }

    }
}
