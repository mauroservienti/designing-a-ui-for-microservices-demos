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
    }
}
