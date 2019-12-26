using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManagement.Services.Warehouse
{
    public interface IProductCreator
    {
        Product Create(CreateProductModel model);
    }

    public class ProductCreator : IProductCreator
    {
        private readonly IProductRepository _productRepository;

        public ProductCreator(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public Product Create(CreateProductModel model)
        {
            var product = new Product
            {
                Name = model.Name
            };

            _productRepository.Create(product);
            _productRepository.SaveChange();

            return product;
        }
    }
}
