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
    public class WarehouseTests : IDisposable
    {
        private readonly WarehouseManagerWebApplicationFactory<Startup> _warehouseManagerWebApplicationFactory = new WarehouseManagerWebApplicationFactory<Startup>();
        private readonly HttpClient _httpClient;

        public WarehouseTests()
        {
            _httpClient = _warehouseManagerWebApplicationFactory.CreateClient();
        }

        [Fact]
        public async void Get()
        {
            var dbContext = _warehouseManagerWebApplicationFactory.GetDbContext();
            dbContext.Add(new Warehouse());
            dbContext.SaveChanges();

            var warehouses = await WarehouseTestHelper.Get(_httpClient);

            Assert.NotNull(warehouses);
            Assert.NotEmpty(warehouses);
        }

        [Fact]
        public async void Create()
        {
            var dbContext = _warehouseManagerWebApplicationFactory.GetDbContext();

            var model = new CreateWarehouseModel()
            {
                Name = Guid.NewGuid().ToString()
            };
            var warehouse = await WarehouseTestHelper.Create(_httpClient, model);

            Assert.Equal(warehouse.Name, model.Name);
            Assert.Contains(dbContext.Warehouses, o => o.Id == warehouse.Id && o.Name == warehouse.Name);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _warehouseManagerWebApplicationFactory.Dispose();
        }
    }
}
