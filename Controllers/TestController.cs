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
    public async Task<double?> Admin()
    {
        using var db = new ProductContext();
        
        
        var res = await db.ProductPriceHistory.Where(x => x.Id == 1).FirstAsync();

        return res?.Price;
    }
    [HttpGet("Read1")]
    public async Task<double?> Read1(int productcount = 100000)
    {
        using var db = new ProductContext();
        var rnd = new Random();
        var i = rnd.Next(productcount * 1000) + 1;
        var res = await db.ProductPriceHistory.Where(x => x.Id == i).FirstAsync();
        
        return res?.Price ;
    }
    [HttpGet("LinearRead1000")]
    public async Task<double?> LinearRead1000(int productcount = 100000)
    {
        using var db = new ProductContext();
        var rnd = new Random();
        var i = rnd.Next(productcount * 1000 - 1000) + 1;
        var res = await db.ProductPriceHistory.Where(x => x.Id >= i && x.Id < i + 1000).AverageAsync(x => x.Price);
        

        return res;
    }
    [HttpGet("RandomRead1000")]
    public async Task<double?> RandomRead1000(int productcount = 100000)
    {
        using var db = new ProductContext();
        var rnd = new Random();
        var i = rnd.Next(productcount) + 1;
        var res = await db.ProductPriceHistory.Where(x => x.ProductId == i).AverageAsync(x => x.Price);


        return res;
    }
    [HttpGet("NonIndexedRead")]
    public async Task<double?> NonIndexedRead(int productcount = 100000)
    {
        using var db = new ProductContext();
        var rnd = new Random();
        var i = rnd.Next(1000);
        var d = DateTime.SpecifyKind(new DateTime(2000, 01, 02), DateTimeKind.Utc).AddDays(i);
        var p = rnd.Next(4000) + 1000;
        var res = await db.ProductPriceHistory.Where(x => x.Price >= p && x.Price < p + 2 && x.ValidFrom >= d && x.ValidFrom < d.AddDays(1)).CountAsync();


        return res;
    }
    [HttpGet("Cpu100000")]
    public async Task<double?> Cpu100000(int productcount = 100000)
    {
        using var db = new ProductContext();
        
        var res = await db.ProductPriceHistory.Where(x => x.Id <= 100000).OrderBy(x => x.Price).Skip(50000).FirstAsync();


        return res.Price;
    }
    [HttpGet("Write1")]
    public async Task<double?> Write1(int productcount = 100000)
    {
        using var db = new ProductContext();
        var rnd = new Random();
        var i = rnd.Next(productcount * 1000) + 1;
        var pph = await db.ProductPriceHistory.Where(x => x.Id == i).FirstAsync();
        pph.PriceChange = pph.PriceChangeInt + 1;
        db.SaveChanges();

        return pph?.Price;
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
