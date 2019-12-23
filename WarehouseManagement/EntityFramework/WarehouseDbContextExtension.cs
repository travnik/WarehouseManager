using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseManagement.EntityFramework
{
    public static class WarehouseDbContextExtension
    {
        public static void EnsureSeedData(this WarehouseDbContext context)
        {

        }

        public static bool AllMigrationsApplied(this WarehouseDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
