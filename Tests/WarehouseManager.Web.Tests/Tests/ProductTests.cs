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
    public class ProductTests : IDisposable
    {
        private readonly WarehouseManagerWebApplicationFactory<Startup> _warehouseManagerWebApplicationFactory = new WarehouseManagerWebApplicationFactory<Startup>();
        private readonly HttpClient _httpClient;

        public ProductTests()
        {
            _httpClient = _warehouseManagerWebApplicationFactory.CreateClient();
        }

        [Fact]
        public async void Get()
        {
            var dbContext = _warehouseManagerWebApplicationFactory.GetDbContext();
            dbContext.Add(new Product());
            dbContext.SaveChanges();

            var products = await ProductTestHelper.Get(_httpClient);

            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public async void Create()
        {
            var dbContext = _warehouseManagerWebApplicationFactory.GetDbContext();

            var model = new CreateProductModel()
            {
                Name = Guid.NewGuid().ToString()
            };

            var product = await ProductTestHelper.Create(_httpClient, model);

            Assert.Equal(product.Name, model.Name);
            Assert.Contains(dbContext.Products, o => o.Id == product.Id && o.Name == product.Name);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _warehouseManagerWebApplicationFactory.Dispose();
        }
    }
}
