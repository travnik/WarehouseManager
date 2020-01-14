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
    public static class WarehouseProductTestHelper
    {
        public static async Task<IEnumerable<WarehouseProduct>> Get(HttpClient client)
        {
            var response = await client.GetAsync("api/WarehouseProduct");
            var responseString = await GetResponseStringAsync(response);

            return JsonConvert.DeserializeObject<IEnumerable<WarehouseProduct>>(responseString);
        }

        public static async Task<WarehouseProduct> Create(HttpClient client, CreateWarehouseProductModel model)
        {
            var content = CreateContent(model);
            var response = await client.PostAsync("api/WarehouseProduct", content);
            var responseString = await GetResponseStringAsync(response);

            return JsonConvert.DeserializeObject<WarehouseProduct>(responseString);
        }

        private static async Task<string> GetResponseStringAsync(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        private static StringContent CreateContent(CreateWarehouseProductModel model)
        {
            return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }
    }
}
