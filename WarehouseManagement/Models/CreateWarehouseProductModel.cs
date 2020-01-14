using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Models
{
    public class CreateWarehouseProductModel
    {
        public CreateProductModel ProductModel { get; set; }

        public CreateWarehouseModel WarehouseModel { get; set; }

        public int Count { get; set; }
    }
}
