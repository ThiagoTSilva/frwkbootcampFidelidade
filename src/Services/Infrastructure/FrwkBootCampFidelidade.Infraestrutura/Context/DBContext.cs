using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.BonificationContext.Entities;
using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using FrwkBootCampFidelidade.Dominio.ExtractContext.Entities;
using FrwkBootCampFidelidade.Infraestrutura.BonificationContext.Mapping;
using FrwkBootCampFidelidade.Infraestrutura.RansomContext.Mapping;
using FrwkBootCampFidelidade.Infraestrutura.OrderItemContext.Mapping;
using Microsoft.EntityFrameworkCore;
using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Mapping;
using FrwkBootCampFidelidade.Infraestrutura.ExtractContext.Mapping;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;
using FrwkBootCampFidelidade.Infraestrutura.Data.PromotionContext.Mapping;

namespace FrwkBootCampFidelidade.Infraestrutura.Context
{
    public class DBContext : DbContext, IDBContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public virtual DbSet<Bonification> Bonifications { get; set; }
        public virtual DbSet<Ransom> Ransoms { get; set; }
        public virtual DbSet<RansomStatus> RansomStatus { get; set; }
        public virtual DbSet<RansomHistoryStatus> RansomHistoryStatus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<WalletType> WalletTypes { get; set; }
        public virtual DbSet<WalletHistoryTransfer> WalletHistoryTransfers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BonificationMap());
            builder.ApplyConfiguration(new RansomMap());
            builder.ApplyConfiguration(new RansomStatusMap());
            builder.ApplyConfiguration(new RansomHistoryStatusMap());
            builder.ApplyConfiguration(new OrderItemMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new WalletTypeMap());
            builder.ApplyConfiguration(new WalletMap());
            builder.ApplyConfiguration(new WalletHistoryTransferMap());
        }
    }
}
