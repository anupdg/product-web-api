using AutoMapper;
using GalvProducts.Api.Business;
using GalvProducts.Api.Business.Contracts;
using GalvProducts.Api.Common;
using GalvProducts.Api.Data;
using GalvProducts.Api.Data.Contracts;
using GalvProducts.Api.Services;
using GalvProducts.Api.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace GalvProducts.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<CurrencyConfigOptions>(options => Configuration.GetSection("Currency").Bind(options));
            services.AddSingleton<HttpClient>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IProductsBA, ProductsBA>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<ICurrencyCacheService, CurrencyCacheService>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddSingleton<ICurrencyService, CurrencyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Swagger configuration
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API");
                c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
