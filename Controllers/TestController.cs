using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stx.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stx.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("Empty")]
    public int Empty()
    {
        return 0;
    }
    [HttpGet("Admin")]
    public int Admin()
    {
        using var db = new ProductContext();
        var sql = "SELECT trunc(random() * 100000 + 1)::int AS random_number;";
        
        var result = db.Database.ExecuteSqlRaw(sql);
                        
        return result;
    }

    /*// GET api/<TestController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<TestController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<TestController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<TestController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }*/
}
