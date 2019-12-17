using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using WarehouseManagement.EntityFramework;
using WarehouseManagement.Repositories;

namespace WarehouseManagement.Test
{
    public static class Factory
    {
        public static WarehouseDbContext GetDbContext()
        {
            var options = new AppDbContextOptions
            {
                Provider = DbContextProvider.Postgre,
                ConnectionString = "Host = localhost; Database = WarehousHello_Test; Username = user; Password = 123",
                MigrationsAssemblyProject = "WarehouseManagement",
                MigrationsHistoryTable = "WarehouseManagement"
            };
            return AppDbContext(options);
        }

        private static WarehouseDbContext AppDbContext(AppDbContextOptions options)
        {
            var dbContext = new WarehouseDbContext(Options.Create(options));
            return dbContext;
        }

        public static WarehouseDbContext GetDbContextInMemory()
        {
            var options = new AppDbContextOptions
            {
                Provider = DbContextProvider.InMemory,
                ConnectionString = Guid.NewGuid().ToString()
            };
            return AppDbContext(options);
        }

        public static IWarehouseRepository GetWarehouseRepository(WarehouseDbContext dbContext)
        {
            return new WarehouseRepository(dbContext);
        }
    }
}
