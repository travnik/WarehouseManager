using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WarehouseManagement.Models;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManager.Web.Tests.TestHelper
{
    public static class ProductTestHelper
    {
        public static async Task<IEnumerable<Product>> Get(HttpClient client)
        {
            var response = await client.GetAsync("api/Product");
            var responseString = await GetResponseStringAsync(response);
            
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(responseString);
        }

        public static async Task<Product> Create(HttpClient client, CreateProductModel model)
        {
            var content = CreateContent(model);
            var response = await client.PostAsync("api/Product", content);
            var responseString = await GetResponseStringAsync(response);
            
            return JsonConvert.DeserializeObject<Product>(responseString);
        }

        private static async Task<string> GetResponseStringAsync(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        private static StringContent CreateContent(CreateProductModel model)
        {
            return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }
    }
}
