using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Stx.Models;
using System.Data.Common;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
namespace Stx.Database;

public class ProductContext : DbContext
{
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductHistory> ProductHistory { get; set; }
    public string ConnectionString { get; set; }
    public ProductContext()
    {
        ConnectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_StxRdb") ?? "";
        if (string.IsNullOrWhiteSpace(ConnectionString))
            throw new Exception("Connection string is empty");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(ConnectionString);
        options
           .UseLowerCaseNamingConvention();
    }
}