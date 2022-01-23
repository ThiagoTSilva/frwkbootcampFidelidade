using FrwkBootCampFidelidade.Aplicacao.Configuration;
using FrwkBootCampFidelidade.Infraestrutura.Context;
using FrwkBootCampFidelidade.Infraestrutura.IOC.IOC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Web.BootCampFidelidade.HttpAggregator
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly string KEY = Environment.GetEnvironmentVariable("SecretKey");
        private readonly string DATABASE = Environment.GetEnvironmentVariable("Database");
        private readonly string DBUSER = Environment.GetEnvironmentVariable("DbUser");
        private readonly string DBPASSWORD = Environment.GetEnvironmentVariable("Password");
        private readonly string DATASOURCE = Environment.GetEnvironmentVariable("Datasource");
        
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest);

            
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer($"Data Source={DATASOURCE};Initial Catalog={DATABASE};Persist Security Info=True;User ID={DBUSER};Password={DBPASSWORD}"));

            services.AddServices()
                    .AddDBInjector()
                    .AddHosted()
                    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY)),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Gateway", Version = "v1" });
            });

            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMqConfig"));
            services.Configure<KafkaConfiguration>(configuration.GetSection("KafkaConfig"));
        }

        //public void ConfigureContainer(ContainerBuilder Builder)
        //{
        //    Builder.RegisterModule(new ModuleIOC());
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Gateway v1"));

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
