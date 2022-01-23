using FluentValidation.AspNetCore;
using FrwkBootCampFidelidade.Aplicacao.Configuration;
using FrwkBootCampFidelidade.Dominio.Base.Interfaces;
using FrwkBootCampFidelidade.DTO.PromotionContext;
using FrwkBootCampFidelidade.Infraestrutura.Context;
using FrwkBootCampFidelidade.Infraestrutura.Data.Context;
using FrwkBootCampFidelidade.Infraestrutura.IOC.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace FrwkBootCampFidelidade.Promotion.API
{
    public class Startup
    {

        private readonly string DATASOURCE = Environment.GetEnvironmentVariable("Datasource");
        private readonly string DATABASE = Environment.GetEnvironmentVariable("Database");
        private readonly string DBUSER = Environment.GetEnvironmentVariable("DbUser");
        private readonly string DBPASSWORD = Environment.GetEnvironmentVariable("Password");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<Startup>();
                    x.RegisterValidatorsFromAssemblyContaining<PromotionDTO>();
                });

            //EF MSSQL
            services.AddDbContext<DBContext>(options => options.UseSqlServer($"Data Source={DATASOURCE};Initial Catalog={DATABASE};Persist Security Info=True;User ID={DBUSER};Password={DBPASSWORD}"));


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            services.AddDBInjector();
            services.AddServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FrwkBootCampFidelidade.Promotion.API", Version = "v1" });
            });
        }

        //public void ConfigureContainer(ContainerBuilder Builder)
        //{
        //    Builder.RegisterModule(new ModuleIOC());
        //}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FrwkBootCampFidelidade.Promotion.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSentryTracing();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
