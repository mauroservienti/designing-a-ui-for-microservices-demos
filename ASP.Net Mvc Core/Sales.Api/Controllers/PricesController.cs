using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Sales.Api.Data;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        [HttpGet]
        [Route("product/{id}")]
        public dynamic Get(int id)
        {
            using var db = new SalesContext();
            var item = db.ProductsPrices
                .SingleOrDefault(o => o.Id == id);

            return item;
        }

        [HttpGet]
        [Route("products/{ids}")]
        public IEnumerable<dynamic> Get(string ids)
        {
            using var db = new SalesContext();
            var productIds = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            var items = db.ProductsPrices
                .Where(status => productIds.Any(id => id == status.Id))
                .ToArray();

            return items;
        }
    }
}
