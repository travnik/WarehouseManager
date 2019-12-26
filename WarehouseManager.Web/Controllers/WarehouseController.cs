using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Shared.Domain;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;
using WarehouseManagement.Services.Warehouse;

namespace WarehouseManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseCreator _warehouseCreator;

        public WarehouseController(IWarehouseRepository warehouseRepository, IWarehouseCreator warehouseCreator)
        {
            _warehouseRepository = warehouseRepository;
            _warehouseCreator = warehouseCreator;
        }

        [HttpGet]
        public IEnumerable<Warehouse> Get()
        {
            return _warehouseRepository.Get()?.ToList();
        }

        [HttpPost]
        public Warehouse Create([FromBody]CreateWarehouseModel createModel)
        {
            return _warehouseCreator.Create(createModel);
        }
    }
}