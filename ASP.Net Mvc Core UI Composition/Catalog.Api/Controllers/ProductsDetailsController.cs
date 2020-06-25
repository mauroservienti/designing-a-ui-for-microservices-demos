using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Catalog.Api.Data;

namespace Catalog.Api.Controllers
{
    [Route("api/product-details")]
    [ApiController]
    public class ProductsDetailsController : ControllerBase
    {
        [HttpGet]
        [Route("product/{id}")]
        public dynamic Get(int id)
        {
            using var db = new MarketingContext();
            var item = db.ProductsDetails
                .SingleOrDefault(o => o.Id == id);

            return item;
        }

        [HttpGet]
        [Route("products/{ids}")]
        public IEnumerable<dynamic> Get(string ids)
        {
            using var db = new MarketingContext();
            var productIds = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            var items = db.ProductsDetails
                .Where(status => productIds.Any(id => id == status.Id))
                .ToArray();

            return items;
        }
    }
}
