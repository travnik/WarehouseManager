using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;
using WarehouseManagement.Services.Warehouse;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCreator _productCreator;

        public ProductController(IProductRepository productRepository, IProductCreator productCreator)
        {
            _productRepository = productRepository;
            _productCreator = productCreator;
        }

        public IEnumerable<Product> Get()
        {
            return _productRepository.Get().ToList();
        }

        [HttpPost]
        public Product Create([FromBody]CreateProductModel createModel)
        {
            return _productCreator.Create(createModel);
        }
    }
}
