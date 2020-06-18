using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    }
}
