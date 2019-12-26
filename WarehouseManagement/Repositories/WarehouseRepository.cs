using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Repositories
{
    public interface IWarehouseRepository
    {
        IQueryable<Warehouse> Get();
        Warehouse Create(Warehouse warehouse);
        Warehouse Update(Warehouse warehouse);
        int SaveChange();
    }

    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly WarehouseDbContext _warehouseDbContext;

        public WarehouseRepository(WarehouseDbContext warehouseDbContext)
        {
            _warehouseDbContext = warehouseDbContext ?? throw new ArgumentNullException(nameof(warehouseDbContext));
        }

        public Warehouse Create(Warehouse warehouse)
        {
            _warehouseDbContext.Warehouses.Add(warehouse);
            return warehouse;
        }

        public int SaveChange()
        {
            return _warehouseDbContext.SaveChanges();
        }

        public IQueryable<Warehouse>Get()
        {
            return _warehouseDbContext.Warehouses
                .Include(o => o.WarehouseProducts)
                .ThenInclude(i => i.Product);
        }

        public Warehouse Update(Warehouse warehouse)
        {
            _warehouseDbContext.Warehouses.Update(warehouse);
            return warehouse;
        }
    }
}
