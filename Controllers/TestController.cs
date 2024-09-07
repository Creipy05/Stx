using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Stx.Core;
using Stx.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stx.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    static Random _rnd = new Random();
    static string _connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_StxRdb") ?? "";

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
    /*[HttpGet("Admin")]
    public async Task<double?> Admin()
    {
        using var db = new ProductContext();
        
        
        var res = await db.ProductPriceHistory.Where(x => x.Id == 1).FirstAsync();

        return res?.Price;
    }*/
    [HttpGet("Admin")]
    public async Task<double?> Admin()
    {
        string query = "SELECT price FROM ProductPriceHistory where id = 1";
        double? price = await NpgUtil.RunSimpleQueryAsync<double>(query, "price");
        return price;
    }

    /*[HttpGet("Read")]
    public async Task<double?> Read(int productcount = 100000)
    {
        using var db = new ProductContext();
        var i = _rnd.Next(productcount * 1000) + 1;
        var res = await db.ProductPriceHistory.Where(x => x.Id == i).FirstAsync();
        
        return res?.Price ;
    }*/
    [HttpGet("Read")]
    public async Task<double?> Read(int productcount = 100000)
    {
        var i = _rnd.Next(productcount * 1000) + 1;
        string query = $"SELECT price FROM ProductPriceHistory where id = @id";
        double? price = await NpgUtil.RunSimpleQueryAsync<double>(query, "price", "id", i);
        return price;
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
/*    [HttpGet("Cpu")]
    public async Task<double?> Cpu()
    {
        using var db = new ProductContext();
        var n = 10000;
        var res = await db.ProductPriceHistory.Where(x => x.Id <= n).OrderBy(x => x.Price).Skip(n / 2).FirstAsync();
        return res.Price;
    }*/

    [HttpGet("Cpu")]
    public async Task<double?> Cpu()
    {
        var n = 60000;
        string query = $"WITH random_numbers AS (select trunc(random() * 1000000 + 1)::int AS number FROM generate_series(1, {n})) select percentile_cont(0.5) WITHIN GROUP (ORDER BY number) AS median_value FROM random_numbers;";
        double? price = await NpgUtil.RunSimpleQueryAsync<double>(query, "median_value");
        return price;

    }

    /*[HttpGet("Network")]
    public async Task<double?> Network()
    {
        using var db = new ProductContext();

        var res2 = await db.ProductPriceHistory.Where(x => x.Id <= 1000 && x.Id > 0)
            .Select(x => 
            x.ProductId.ToString() + " " 
            + x.ValidFrom.ToString() + " " 
            + x.Price.ToString() + " " 
            + x.PriceChange.ToString() + " " 
            + x.PriceChangeInt.ToString()
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            )
            .ToArrayAsync();

        var res = await db.ProductPriceHistory
            //.Where(x => x.Id <= 1000 && x.Id > 0)
            .Take(1000)
            .Select(x =>
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            )
            .ToArrayAsync();

        return res.Count();
    }*/

    /*[HttpGet("Network")]
    public async Task<double?> Network()
    {
        var n = 60000;
        string query = $"WITH chars AS (SELECT 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789' AS char_set) SELECT string_agg(substr(char_set, floor(random() * 62 + 1)::int, 1), '') AS random_text FROM generate_series(1, 128*1024), chars;";
        var text = await NpgUtil.RunSimpleQueryAsync<string>(query, "random_text");
        return text?.Length;
    }*/
    [HttpGet("Network")]
    public async Task<double?> Network()
    {
        string query = $"SELECT 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789' as random_text FROM generate_series(1, 1500);";
        var text = await NpgUtil.RunMultirowQueryAsync<string>(query, "random_text");
        return text?.Length;
    }
    /*[HttpPost("Write")]
    public async Task<double?> Write(int productcount = 100000)
    {
       
        var i = _rnd.Next(productcount * 1000) + 1;
        
        var pph = await db.ProductPriceHistory.Where(x => x.Id == i).FirstAsync();
        pph.PriceChange = pph.PriceChangeInt + 1;
        
    }*/

    [HttpPost("MultiWrite")]
    public async Task<double?> MultiWrite(int productcount = 100000)
    {
        var pphIds = new List<long>();
        var firstpphId = _rnd.Next(productcount * 1000-2000) + 1;
        for (int j = 0; j < 333; j++)
        {
            pphIds.Add(firstpphId + j * 3);
            //pphIds.Add(_rnd.Next(productcount * 1000) + 1);
        }

        using var db = new ProductContext();
        var pphs = await db.ProductPriceHistory.Where(x => pphIds.Contains(x.Id)).ToListAsync();
        foreach (var pph in pphs)
        {
            pph.PriceChange = pph.PriceChangeInt + 1;
        }
        /*var i = _rnd.Next(productcount * 1000) + 1;
        
        var pph = await db.ProductPriceHistory.Where(x => x.Id == i).FirstAsync();
        pph.PriceChange = pph.PriceChangeInt + 1;*/
        db.SaveChanges();

        return pphs?.Count;
    }
}
