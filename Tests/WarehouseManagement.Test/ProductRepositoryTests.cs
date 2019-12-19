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
    public class ProductRepositoryTests : IDisposable
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IProductRepository _repository;

        public ProductRepositoryTests()
        {
            _dbContext = Factory.GetDbContextInMemory();
            _repository = Factory.GetProductRepository(_dbContext);
        }

        [Fact]
        public void Get()
        {
            var product = new Product()
            {
                Name = GenarateName()
            };
            _dbContext.Add(product);

            _dbContext.SaveChanges();
            var products = _repository.Get().ToList();

            Assert.Contains(products, o => o.Id == product.Id);
        }

        [Fact]
        public void Get_WhenNotFound()
        {
            var products = _repository.Get().ToList();

            Assert.DoesNotContain(products, o => o.Id == Guid.NewGuid());
        }

        private string GenarateName()
        {
            return Guid.NewGuid().ToString();
        }

        [Fact]
        public void Create()
        {
            var product = new Product()
            {
                Name = GenarateName(),
                WarehouseProducts = new List<WarehouseProduct>()
                {
                    new WarehouseProduct()
                    {
                        Warehouse = new Warehouse()
                    }
                }
            };
            var fact = _repository.Create(product);
            _repository.SaveChange();

            Assert.Contains(_dbContext.Products, o => o.Name == product.Name);
        }

        [Fact]
        public void Update()
        {
            var newName = GenarateName();

            var product = new Product()
            {
                Name = GenarateName()
            };
            _dbContext.Add(product);
            _dbContext.SaveChanges();

            product = _dbContext.Products.First(o => o.Id == product.Id);
            product.Name = newName;

            var fact = _repository.Update(product);
            _repository.SaveChange();

            Assert.Contains(_dbContext.Products, o => o.Id == product.Id && o.Name == newName);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
