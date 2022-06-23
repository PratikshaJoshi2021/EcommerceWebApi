using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        EcommerceContext db;
        public ProductController(EcommerceContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products;
        }

        [HttpPost]
        public string Post([FromBody] Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return "success";
        }

    }
}
