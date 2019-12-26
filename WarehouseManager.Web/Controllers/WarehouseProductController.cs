using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProductController : ControllerBase
    {
        private static readonly List<WarehouseProduct> _warehouseProducts = new List<WarehouseProduct>()
        {
            Create("первый"),
            Create("второй"),
            Create("третий")
        };

        [HttpGet]
        public IEnumerable<WarehouseProduct> Get()
        {
            return _warehouseProducts.ToList();
        }

        private static WarehouseProduct Create(string name)
        {
            return new WarehouseProduct()
            {
                Warehouse = new Warehouse()
                {
                    Name = $"склад {name}"
                },
                Product = new Product()
                {
                    Name = $"продукт {name}"
                },
                Count = 4,
                Id = Guid.NewGuid()
            };
        }
    }
}