using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WarehouseManagement.Domain;
using WarehouseManagement.Models;

namespace WarehouseManager.Web.Tests.TestHelper
{
    public static class WarehouseTestHelper
    {
        public static async Task<IEnumerable<Warehouse>> Get(HttpClient client)
        {
            var response = await client.GetAsync("api/Warehouse");
            var responseString = await GetResponseStringAsync(response);
            return JsonConvert.DeserializeObject<IEnumerable<Warehouse>>(responseString);
        }

        public static async Task<Warehouse> Create(HttpClient client, CreateWarehouseModel model)
        {
            var content = CreateContent(model);
            var response = await client.PostAsync("api/Warehouse", content);
            var responseString = await GetResponseStringAsync(response);
            return JsonConvert.DeserializeObject<Warehouse>(responseString);
        }

        private static async Task<string> GetResponseStringAsync(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        private static StringContent CreateContent(CreateWarehouseModel model)
        {
            return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }
    }
}
