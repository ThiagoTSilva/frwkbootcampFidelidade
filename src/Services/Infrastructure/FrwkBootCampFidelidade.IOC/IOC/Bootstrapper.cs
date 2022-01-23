using FrwkBootCampFidelidade.Aplicacao.Consumers;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService;
using FrwkBootCampFidelidade.Aplicacao.Producer;
using FrwkBootCampFidelidade.Aplicacao.Services;
using FrwkBootCampFidelidade.Aplicacao.Services.RpcService;
using Microsoft.Extensions.DependencyInjection;

namespace FrwkBootCampFidelidade.Infraestrutura.IOC.IOC
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IRpcClientService, RpcClientService>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IBonificationService, BonificationService>()
                .AddTransient<IWalletService, WalletService>()
                .AddTransient<IRansomService, RansomService>()
                .AddTransient<IPromotionService, PromotionService>()
                .AddTransient<IExtractService, ExtractService>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IKafkaProducerService, KafkaProducerService>();

            return services;
        }

        public static IServiceCollection AddHosted(this IServiceCollection services)
        {
            services
                .AddHostedService<PromotionConsumer>()
                .AddHostedService<BonificationConsumer>()
                .AddHostedService<ExtractConsumer>()
                .AddHostedService<RansomConsumer>()
                .AddHostedService<WalletConsumer>()
                .AddHostedService<OrderConsumer>();

            return services;
        }
    }
}
