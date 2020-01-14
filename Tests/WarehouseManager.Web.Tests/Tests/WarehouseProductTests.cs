using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WarehouseManagement.Models;
using WarehouseManagement.Shared.Domain;
using WarehouseManager.Web.Tests.Helpers;
using WarehouseManager.Web.Tests.TestHelper;
using Xunit;

namespace WarehouseManager.Web.Tests.Tests
{
    public class WarehouseProductTests : IDisposable
    {
        private readonly WarehouseManagerWebApplicationFactory<Startup> _warehouseManagerWebApplicationFactory = new WarehouseManagerWebApplicationFactory<Startup>();
        private readonly HttpClient _httpClient;

        public WarehouseProductTests()
        {
            _httpClient = _warehouseManagerWebApplicationFactory.CreateClient();
        }

        [Fact]
        public async void Get()
        {
            var dbContext = _warehouseManagerWebApplicationFactory.GetDbContext();
            dbContext.Add(new Warehouse());
            dbContext.SaveChanges();

            var warehouseProducts = await WarehouseProductTestHelper.Get(_httpClient);

            Assert.NotNull(warehouseProducts);
            Assert.NotEmpty(warehouseProducts);
        }

        [Fact]
        public async void Create()
        {
            var dbContext = _warehouseManagerWebApplicationFactory.GetDbContext();

            var model = new CreateWarehouseProductModel()
            {
                WarehouseModel = new CreateWarehouseModel() { Name = Guid.NewGuid().ToString() },
                ProductModel = new CreateProductModel() { Name = Guid.NewGuid().ToString() },
                Count = 10
            };

            var warehouseProduct = await WarehouseProductTestHelper.Create(_httpClient, model);

            Assert.Equal(warehouseProduct.Warehouse.Name, model.WarehouseModel.Name);
            Assert.Equal(warehouseProduct.Product.Name, model.ProductModel.Name);
            Assert.Equal(warehouseProduct.Count, model.Count);

            Assert.Contains(dbContext.WarehouseProducts, o => o.Id == warehouseProduct.Id && 
                                                                    o.Warehouse.Name == warehouseProduct.Warehouse.Name && 
                                                                    o.Product.Name == warehouseProduct.Product.Name &&
                                                                    o.Count == warehouseProduct.Count);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _warehouseManagerWebApplicationFactory.Dispose();
        }
    }
}
