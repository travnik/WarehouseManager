using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.EntityFramework;

namespace WarehouseManager.Web.Tests.Helpers
{
    public class WarehouseManagerWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        private const string AppSettingJson = "appsettings.Test.json";

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureAppConfiguration(AddJsonFiles);
            builder.UseSolutionRelativeContentRoot("");
            builder.UseStartup<TEntryPoint>();
        }

        private void AddJsonFiles(IConfigurationBuilder configBuilder)
        {
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(AppSettingJson);
        }

        public WarehouseDbContext GetDbContext()
        {
            return Services.CreateScope().ServiceProvider.GetService<WarehouseDbContext>();
        }
    }
}
