using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Services;
using CCPPC.VirtualShop.Data.Context;
using CCPPC.VirtualShop.Data.Repositories;
using CCPPC.VirtualShop.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CCPPC.VirtualShop.Api.Configuration
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")));

            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IProductRepository, ProductRepository>();

            service.AddControllers();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
        }
    }
}
