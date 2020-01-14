using System;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;
using WarehouseManagement.Shared.Domain;

namespace WarehouseManagement.Services.Warehouse
{
    public interface IWarehouseProductCreator
    {
        WarehouseProduct Create(CreateWarehouseProductModel model);
    }

    public class WarehouseProductCreator : IWarehouseProductCreator
    {
        private readonly IWarehouseProductRepository _warehouseProductRepository;

        public WarehouseProductCreator(IWarehouseProductRepository warehouseProductRepository)
        {
            _warehouseProductRepository = warehouseProductRepository ?? throw new ArgumentNullException(nameof(warehouseProductRepository));
        }

        public WarehouseProduct Create(CreateWarehouseProductModel model)
        {
            var warehouseProduct = new WarehouseProduct
            {
                Warehouse = new Shared.Domain.Warehouse { Name = model.WarehouseModel.Name },
                Product = new Shared.Domain.Product() { Name = model.ProductModel.Name },
                Count = model.Count
            };

            _warehouseProductRepository.Create(warehouseProduct);
            _warehouseProductRepository.SaveChange();

            return warehouseProduct;
        }


    }
}
