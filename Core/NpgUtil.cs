using Npgsql;

namespace Stx.Core;

public static class NpgUtil
{
    static Random _rnd = new Random();
    static string _connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_StxRdb") ?? "";

    public static async Task<T?> RunMultirowQueryAsync<T>(string query, string fieldName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(query, connection);
        await using var reader = await command.ExecuteReaderAsync();
        //var results = new List<T>();
        T res = default;
        //T res = null;
        while (await reader.ReadAsync())
        {
            res = reader.GetFieldValue<T>(reader.GetOrdinal(fieldName));
            //results.Add(res);
        }
        //await reader.ReadAsync();
        return res;
    }

    public static async Task<T?> RunSimpleQueryAsync<T>(string query, string fieldName)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(query, connection);
        await using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        var res = reader.GetFieldValue<T>(reader.GetOrdinal(fieldName));
        return res;
    }

    public static async Task<T?> RunSimpleQueryAsync<T>(string query, string fieldName, string parameterName, object parameterValue)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(query, connection);

        command.Parameters.AddWithValue(parameterName, parameterValue);

        await using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        var res = reader.GetFieldValue<T>(reader.GetOrdinal(fieldName));
        return res;
    }

    public static async Task<T?> RunSimpleQueryAsync<T>(string query, string fieldName, Dictionary<string, object> parameters)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(query, connection);

        foreach (var param in parameters)
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }

        await using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        var res = reader.GetFieldValue<T>(reader.GetOrdinal(fieldName));
        return res;
    }

}
