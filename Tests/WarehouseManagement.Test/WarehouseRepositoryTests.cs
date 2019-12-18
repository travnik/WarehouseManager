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
    public class WarehouseRepositoryTests : IDisposable
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IWarehouseRepository _repository;

        public WarehouseRepositoryTests()
        {
            _dbContext = Factory.GetDbContextInMemory();
            _repository = Factory.GetWarehouseRepository(_dbContext);
        }

        [Fact]
        public void Get()
        {
            var warehouse = new Warehouse()
            {
                Name = GenarateName()
            };
            _dbContext.Add(warehouse);

            _dbContext.SaveChanges();
            var warehouses = _repository.Get().ToList();

            Assert.Contains(warehouses, o => o.Id == warehouse.Id);
        }

        [Fact]
        public void Get_WhenNotFound()
        {
            var warehouses = _repository.Get().ToList();

            Assert.DoesNotContain(warehouses, o => o.Id == Guid.NewGuid());
        }

        private string GenarateName()
        {
            return Guid.NewGuid().ToString();
        }

        [Fact]
        public void Create()
        {
            var warehouse = new Warehouse()
            {
                Name = GenarateName(),
                WarehouseProducts = new List<WarehouseProduct>()
                {
                    new WarehouseProduct()
                    {
                        Product = new Product()
                    }
                }
            };
            var fact = _repository.Create(warehouse);
            _repository.SaveChange();

            Assert.Contains(_dbContext.Warehouses, o => o.Name == warehouse.Name);
        }

        [Fact]
        public void Update()
        {
            var newName = GenarateName();

            var warehouse = new Warehouse()
            {
                Name = GenarateName()
            };
            _dbContext.Add(warehouse);
            _dbContext.SaveChanges();

            warehouse = _dbContext.Warehouses.First(o => o.Id == warehouse.Id);
            warehouse.Name = newName;

            var fact = _repository.Update(warehouse);
            _repository.SaveChange();

            Assert.Contains(_dbContext.Warehouses, o => o.Id == warehouse.Id && o.Name == newName);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
