using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.EntityFramework;
using WarehouseManagement.Repositories;
using WarehouseManagement.Services.Warehouse;

namespace WarehouseManagement
{
    public static class WarehouseManagementExtension
    {
        public static void ConfigureWarehouseManagement(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseDbContext>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IWarehouseCreator, WarehouseCreator>();
            services.Configure<AppDbContextOptions>(o => configuration.GetSection("DbContextOptions").Bind(o));
        }

        public static void WarehouseInitializeDatabase(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetRequiredService<Microsoft.Extensions.Options.IOptions<AppDbContextOptions>>();
            var isMemory = options.Value.Provider == DbContextProvider.InMemory;
            InitializeDatabase(app, isMemory);
        }

        private static void InitializeDatabase(IApplicationBuilder app, bool isMemory)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<WarehouseDbContext>();
                if (isMemory)
                {
                    context.EnsureSeedData();
                }
                else
                {
                    context.Database.Migrate();
                    if (context.AllMigrationsApplied())
                    {
                        context.EnsureSeedData();
                    }
                }
            }
        }
    }
}
