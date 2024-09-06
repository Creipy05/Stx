using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Stx.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stx.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestNpgController : ControllerBase
{
    static Random _rnd = new Random();
    static string _connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_StxRdb") ?? "";

    [HttpGet("Empty")]
    public int Empty()
    {
        return 0;
    }
    [HttpGet("Admin")]
    public async Task<double?> Admin()
    {
        
        string query = "SELECT * FROM ProductPriceHistory where id = 1";
        double? price = await RunSimpleQueryAsync<double>(query, "price");
        return price;
    }

    private static async Task<T?> RunSimpleQueryAsync<T>(string query, string fieldName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(query, connection);
        await using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        var res = reader.GetFieldValue<T>(reader.GetOrdinal(fieldName));
        return res;
    }

}
