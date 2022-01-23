using FrwkBootCampFidelidade.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Dominio.WalletContext.Entities
{
    public class WalletType : EntityBase
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
