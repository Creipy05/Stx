using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stx.Database;
using Stx.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        

        

        // POST api/<AdminController>
        [HttpPost("Init")]
        public void Init([FromQuery] int productCount = 1)
        {
            using var db = new ProductContext();
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            var chunkSize = 2;
            for (int j = 0; j < productCount / chunkSize; j++)
            {
                using var db2 = new ProductContext();
                for (int i = 1; i <= chunkSize; i++)
                {
                    var act = j * chunkSize + i;
                    var p = RandomProduct.GenerateProduct(act, productCount);
                    db2.Add(p);
                }
                db2.SaveChanges();
            }
            
            
            
        }
    }
}
