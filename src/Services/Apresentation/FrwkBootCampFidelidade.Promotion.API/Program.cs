using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FrwkBootCampFidelidade.Promotion.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSentry(o => {
                        o.Dsn = "https://94ba1391f0f743328a3479bc8b5dd377@o1098452.ingest.sentry.io/6122766";
                        // When configuring for the first time, to see what the SDK is doing:
                        o.Debug = true;
                        // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                        // We recommend adjusting this value in production.
                        o.TracesSampleRate = 1.0;
                    });
                });
    }
}
