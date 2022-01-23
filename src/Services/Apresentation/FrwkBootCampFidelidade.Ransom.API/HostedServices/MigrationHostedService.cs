using FrwkBootCampFidelidade.Infraestrutura.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Ransom.API.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public MigrationHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<DBContext>();
            await ctx.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
