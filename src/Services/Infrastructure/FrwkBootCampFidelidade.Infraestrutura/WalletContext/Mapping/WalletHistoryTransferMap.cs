using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Mapping
{
    public class WalletHistoryTransferMap : IEntityTypeConfiguration<WalletHistoryTransfer>
    {
        public void Configure(EntityTypeBuilder<WalletHistoryTransfer> builder)
        {
            builder.ToTable("WalletHistoryTransfer");
        }
 
    }
}
