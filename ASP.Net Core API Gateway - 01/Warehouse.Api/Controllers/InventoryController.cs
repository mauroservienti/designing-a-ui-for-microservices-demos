using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Api.Data;

namespace Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet]
        [Route("product/{id}")]
        public dynamic Get(int id)
        {
            using var db = new WarehouseContext();
            var item = db.StockItems
                .SingleOrDefault(o => o.ProductId == id);

            return item;
        }
    }
}
