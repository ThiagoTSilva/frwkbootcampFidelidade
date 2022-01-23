using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.Dominio.BonificationContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.ExtractContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.OrderContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.RansomContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.WalletContext.Interfaces;
using FrwkBootCampFidelidade.Infraestrutura.BonificationContext.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Data.Context;
using FrwkBootCampFidelidade.Infraestrutura.Data.PromotionContext.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Repository;
using FrwkBootCampFidelidade.Infraestrutura.ExtractContext.Repository;
using FrwkBootCampFidelidade.Infraestrutura.OrderItemContext.Repository;
using FrwkBootCampFidelidade.Infraestrutura.RansomContext.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FrwkBootCampFidelidade.Infraestrutura.IOC.IOC
{
    public static class DBInjector
    {
        public static IServiceCollection AddDBInjector(this IServiceCollection services)
        {
            services.AddTransient<IBonificationRepository, BonificationRepository>();
            services.AddTransient<IRansomRepository, RansomRepository>();
            services.AddTransient<IRansomHistoryStatusRepository, RansomHistoryStatusRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<IWalletTypeRepository, WalletTypeRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IWalletHistoryTransferRepository, WalletHistoryTransferRepository>();
            services.AddTransient<IMongoContext, MongoContext>();

            return services;
        }
    }
}
