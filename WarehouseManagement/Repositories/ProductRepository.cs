using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManagement.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Get();
        Product Create(Product product);
        Product Update(Product product);
        int SaveChange();
    }

    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseDbContext _warehouseDbContext;

        public ProductRepository(WarehouseDbContext warehouseDbContext)
        {
            _warehouseDbContext = warehouseDbContext ?? throw new ArgumentNullException(nameof(warehouseDbContext));
        }

        public Product Create(Product product)
        {
            _warehouseDbContext.Products.Add(product);
            return product;
        }

        public int SaveChange()
        {
            return _warehouseDbContext.SaveChanges();
        }

        public IQueryable<Product>Get()
        {
            return _warehouseDbContext.Products
                .Include(o => o.WarehouseProducts)
                .ThenInclude(o => o.Warehouse);
        }

        public Product Update(Product product)
        {
            _warehouseDbContext.Products.Update(product);
            return product;
        }
    }
}
