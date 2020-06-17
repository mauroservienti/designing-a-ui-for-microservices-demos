using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Catalog.Api.Data;

namespace Catalog.Api.Controllers
{
    [Route("api/available")]
    [ApiController]
    public class AvailableProductsController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public IEnumerable<int> Get()
        {
            using (var db = new MarketingContext())
            {
                var all = db.ProductsDetails
                    .Select(p => p.Id)
                    .ToArray();

                return all;
            }
        }
    }
}
