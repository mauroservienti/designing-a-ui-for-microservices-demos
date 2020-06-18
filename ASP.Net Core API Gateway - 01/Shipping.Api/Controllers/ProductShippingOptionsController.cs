using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shipping.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shipping.Api.Controllers
{
    [Route("api/shipping-options")]
    [ApiController]
    public class ProductShippingOptionsController : ControllerBase
    {
        [HttpGet]
        [Route("product/{id}")]
        public dynamic Get(int id)
        {
            using var db = new ShippingContext();
            var item = db.ProductShippingOptions
                .Include(pso => pso.Options)
                .SingleOrDefault(o => o.ProductId == id);

            return item;
        }
    }
}
