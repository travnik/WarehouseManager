using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManagement.Repositories
{
    public interface IWarehouseProductRepository
    {
        IQueryable<WarehouseProduct> Get();
        WarehouseProduct Get(Guid warehouseId, Guid productId);
        WarehouseProduct Create(WarehouseProduct warehouse);
        WarehouseProduct Update(WarehouseProduct warehouse);
        int SaveChange();
    }

    public class WarehouseProductRepository : IWarehouseProductRepository
    {
        private readonly WarehouseDbContext _warehouseDbContext;

        public WarehouseProductRepository(WarehouseDbContext warehouseDbContext)
        {
            _warehouseDbContext = warehouseDbContext;
        }

        public WarehouseProduct Create(WarehouseProduct warehouseProduct)
        {
            _warehouseDbContext.WarehouseProducts.Add(warehouseProduct);
            return warehouseProduct;
        }

        public int SaveChange()
        {
            return _warehouseDbContext.SaveChanges();
        }

        public IQueryable<WarehouseProduct> Get()
        {
            return _warehouseDbContext.WarehouseProducts;
        }

        public WarehouseProduct Update(WarehouseProduct warehouseProduct)
        {
            _warehouseDbContext.WarehouseProducts.Update(warehouseProduct);
            return warehouseProduct;
        }

        public void Delete(Guid warehouseId, Guid productId)
        {
            var warehouse = Get(warehouseId, productId);
            _warehouseDbContext.WarehouseProducts.Remove(warehouse);
        }

        public WarehouseProduct Get(Guid warehouseId, Guid productId)
        {
            return Get().First(o => o.ProductId == productId && o.WarehouseId == warehouseId);
        }
    }
}
