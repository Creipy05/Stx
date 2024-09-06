using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stx.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stx.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    static Random _rnd = new Random();
    /*private readonly ProductContext _context;

    public TestController(ProductContext context)
    {
        _context = context;
    }*/

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
    
    [HttpGet("Read")]
    public async Task<double?> Read(int productcount = 100000)
    {
        using var db = new ProductContext();
        var i = _rnd.Next(productcount * 1000) + 1;
        var res = await db.ProductPriceHistory.Where(x => x.Id == i).FirstAsync();
        
        return res?.Price ;
    }

    [HttpGet("LinearRead")]
    public async Task<double?> LinearRead(int productcount = 100000)
    {
        using var db = new ProductContext();
        var i = _rnd.Next(productcount * 1000 - 1000) + 1;
        var res = await db.ProductPriceHistory.Where(x => x.Id >= i && x.Id < i + 1000).AverageAsync(x => x.Price);
        

        return res;
    }
    [HttpGet("RandomRead")]
    public async Task<double?> RandomRead(int productcount = 100000)
    {
        using var db = new ProductContext();
        var i = _rnd.Next(productcount) + 1;
        var res = await db.ProductPriceHistory.Where(x => x.ProductId == i).AverageAsync(x => x.Price);


        return res;
    }
    [HttpGet("NonIndexedRead")]
    public async Task<double?> NonIndexedRead(int productcount = 100000)
    {
        using var db = new ProductContext();
        var i = _rnd.Next(1000);
        var d = DateTime.SpecifyKind(new DateTime(2000, 01, 02), DateTimeKind.Utc).AddDays(i);
        var p = _rnd.Next(4000) + 1000;
        var res = await db.ProductPriceHistory.Where(x => x.Price >= p && x.Price < p + 2 && x.ValidFrom >= d && x.ValidFrom < d.AddDays(1)).CountAsync();


        return res;
    }
    [HttpGet("Cpu")]
    public async Task<double?> Cpu()
    {
        using var db = new ProductContext();
        
        var res = await db.ProductPriceHistory.Where(x => x.Id <= 10000).OrderBy(x => x.Price).Skip(5000).FirstAsync();


        return res.Price;
    }

    [HttpGet("Network")]
    public async Task<double?> Network()
    {
        using var db = new ProductContext();

        var res = await db.ProductPriceHistory.Where(x => x.Id <= 1000).Select(x => x.ProductId.ToString() + " " + x.ValidFrom.ToString() + " " + x.Price.ToString() + " " + x.PriceChange.ToString()).ToArrayAsync();


        return res.Count();
    }

    [HttpPost("Write")]
    public async Task<double?> Write(int productcount = 100000)
    {
        using var db = new ProductContext();
        var i = _rnd.Next(productcount * 1000) + 1;
        var pph = await db.ProductPriceHistory.Where(x => x.Id == i).FirstAsync();
        pph.PriceChange = pph.PriceChangeInt + 1;
        db.SaveChanges();

        return pph?.Price;
    }
}
