using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FrwkBootCampFidelidade.Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSentry(o => {
                        o.Dsn = "https://94ba1391f0f743328a3479bc8b5dd377@o1098452.ingest.sentry.io/6122766";
                        o.Debug = true;
                        o.TracesSampleRate = 1.0;
                    });
                });
    }
}
