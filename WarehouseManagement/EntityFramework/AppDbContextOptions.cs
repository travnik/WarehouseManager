using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.EntityFramework
{
    public enum DbContextProvider
    {
        Postgre,
        InMemory
    }

    public class AppDbContextOptions
    {
        public string ConnectionString { get; set; }
        public string MigrationsAssemblyProject { get; set; }
        public string MigrationsHistoryTable { get; set; }
        public DbContextProvider Provider { get; set; }

        public void SetOptions(DbContextOptionsBuilder options)
        {
            switch (Provider)
            {
                case DbContextProvider.Postgre:
                    options.UseNpgsql(ConnectionString, b => b.MigrationsAssembly(MigrationsAssemblyProject).MigrationsHistoryTable(MigrationsHistoryTable));
                    break;
                case DbContextProvider.InMemory:
                    options.UseInMemoryDatabase(ConnectionString);
                    break;
                default:
                    options.UseNpgsql(ConnectionString, b => b.MigrationsAssembly(MigrationsAssemblyProject).MigrationsHistoryTable(MigrationsHistoryTable));
                    break;
            }
        }
    }
}
