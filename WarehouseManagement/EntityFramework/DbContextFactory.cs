using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace WarehouseManagement.EntityFramework
{
    public class DbContextFactory : IDesignTimeDbContextFactory<WarehouseDbContext>
    {
        public WarehouseDbContext CreateDbContext(string[] args)
        {
            var options = new AppDbContextOptions
            {
                Provider = DbContextProvider.Postgre,
                ConnectionString = "Host = localhost; Database = Warehouse; Username = postgres; Password = postgres",
                MigrationsAssemblyProject = "WarehouseManagement",
                MigrationsHistoryTable = "WarehouseManagement"
            };
            return new WarehouseDbContext(Options.Create(options));
        }
    }
}
