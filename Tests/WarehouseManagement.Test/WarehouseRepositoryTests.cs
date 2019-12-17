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
            _dbContext.Add(new Warehouse()
            {
                Name = "test"
            });

            _dbContext.SaveChanges();
            var warehouses = _repository.Get().ToList();

            Assert.NotNull(warehouses);
            Assert.True(warehouses.Any());
        }

        [Fact]
        public void Create()
        {
            var warehouse = new Warehouse()
            {
                Name = "test",
                WarehouseProducts = new List<WarehouseProduct>()
                {
                    new WarehouseProduct()

                    {
                        Product = new Product()
                    }
                }
            };
            var fact = _repository.Create(warehouse);

            Assert.Contains(_dbContext.Warehouses, o => o.Name == warehouse.Name);
        }

        [Fact]
        public void Update()
        {
            var newName = "new_name";

            var warehouse = new Warehouse()
            {
                Name = "old_name"
            };
            _dbContext.Add(warehouse);
            _dbContext.SaveChanges();

            warehouse = _dbContext.Warehouses.First(o => o.Id == warehouse.Id);
            warehouse.Name = newName;



            //var warehouses = _repository.Get().ToList();

            //Assert.NotNull(warehouses);
            //Assert.True(warehouses.Any());
            Assert.Contains(_dbContext.Warehouses, o => o.Id == warehouse.Id && o.Name == newName);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
