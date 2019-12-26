using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;

namespace WarehouseManagement.Services.Warehouse
{
    public interface IWarehouseCreator
    {
        Shared.Domain.Warehouse Create(CreateWarehouseModel model);
    }

    public class WarehouseCreator : IWarehouseCreator
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseCreator(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository)); ;
        }

        public Shared.Domain.Warehouse Create(CreateWarehouseModel model)
        {
            var warehouse = new Shared.Domain.Warehouse
            {
                Name = model.Name
            };

            _warehouseRepository.Create(warehouse);
            _warehouseRepository.SaveChange();

            return warehouse;
        }
    }
}
