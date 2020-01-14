using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;
using WarehouseManagement.Services.Warehouse;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProductController : ControllerBase
    {
        private IWarehouseProductRepository _warehouseProductRepository;
        private IWarehouseProductCreator _warehouseProductCreator;

        public WarehouseProductController(IWarehouseProductRepository warehouseProductRepository, IWarehouseProductCreator warehouseProductCreator)
        {
            _warehouseProductRepository = warehouseProductRepository;
            _warehouseProductCreator = warehouseProductCreator;
        }

        [HttpGet]
        public IEnumerable<WarehouseProduct> Get()
        {
            return _warehouseProductRepository.Get().ToList();
        }

        [HttpPost]
        public WarehouseProduct Create([FromBody] CreateWarehouseProductModel createModel)
        {
            return _warehouseProductCreator.Create(createModel);
        }
    }
}