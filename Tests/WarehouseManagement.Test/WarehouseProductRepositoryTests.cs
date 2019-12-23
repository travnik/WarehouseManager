using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseManagement.Domain;
using WarehouseManagement.EntityFramework;
using WarehouseManagement.Repositories;
using Xunit;

namespace WarehouseManagement.Test
{
    public class WarehouseProductRepositoryTests : IDisposable
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IWarehouseProductRepository _repository;

        public WarehouseProductRepositoryTests()
        {
            _dbContext = Factory.GetDbContextInMemory();
            _repository = Factory.GetWarehouseProductRepository(_dbContext);
        }

        [Fact]
        public void Get()
        {
            var warehouseProduct = new WarehouseProduct()
            {
                ProductId = Guid.NewGuid(),
                WarehouseId = Guid.NewGuid()
            };

            _dbContext.Add(warehouseProduct);
            _dbContext.SaveChanges();

            var warehouseProducts = _repository.Get().ToList();

            Assert.Contains(warehouseProducts, o => o.ProductId == warehouseProduct.ProductId && o.WarehouseId == warehouseProduct.WarehouseId);
        }

        [Fact]
        public void Get_WhenNotFound()
        {
            var warehouseProducts = _repository.Get().ToList();

            Assert.DoesNotContain(warehouseProducts, o => o.ProductId == Guid.NewGuid() && o.WarehouseId == Guid.NewGuid());
        }

        private string GenarateName()
        {
            return Guid.NewGuid().ToString();
        }

        [Fact]
        public void Create()
        {
            var warehouseProduct = new WarehouseProduct()
            {
                Product = new Product(),
                Warehouse = new Warehouse()
            };

            var fact = _repository.Create(warehouseProduct);
            _repository.SaveChange();

            Assert.Contains(_dbContext.WarehouseProducts, o => o.ProductId == warehouseProduct.ProductId);
        }

        [Fact]
        public void Update()
        {
            var newName = GenarateName();
            var newCount = 5;

            var warehouseProduct = new WarehouseProduct()
            {
                ProductId = Guid.NewGuid(),
                WarehouseId = Guid.NewGuid()
            };

            _dbContext.Add(warehouseProduct);
            _dbContext.SaveChanges();

            warehouseProduct = _dbContext.WarehouseProducts.First(o => o.ProductId == warehouseProduct.ProductId);
            warehouseProduct.Count = 5;

            var fact = _repository.Update(warehouseProduct);
            _repository.SaveChange();

            Assert.Contains(_dbContext.WarehouseProducts, o => o.ProductId == warehouseProduct.ProductId && o.WarehouseId == warehouseProduct.WarehouseId && warehouseProduct.Count == newCount);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
