using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;

namespace WarehouseManagement.Services.Warehouse
{
    public interface IProductCreator
    {
        Product Create(CreateProductModel model);
    }

    public class ProductCreator : IProductCreator
    {
        private readonly ProductRepository _productRepository;

        public ProductCreator(ProductRepository productRepository)
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
