﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WarehouseManagement.Domain;

namespace WarehouseManagement.EntityFramework
{
    public class WarehouseDbContext : DbContext
    {
        private readonly AppDbContextOptions _contextOptions;

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }


        public WarehouseDbContext(IOptions<AppDbContextOptions> contextOptions) :base()
        {
            _contextOptions = contextOptions.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseProduct>().HasKey(u => new {u.ProductId, u.WarehouseId});
            modelBuilder.Entity<Warehouse>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Product>().HasKey(u => new { u.Id });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _contextOptions.SetOptions(optionsBuilder);
        }
    }
}
