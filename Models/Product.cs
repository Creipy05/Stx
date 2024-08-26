using System.ComponentModel.DataAnnotations;

namespace Stx.Models;

public class Product
{
    public int Id { get; set; }
    [MaxLength(7)]
    public string? Code { get; set; }
    public string? Name { get; set; }
    public bool? Active { get; set; }
    public double? Price { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? Rating { get; set; }
    //public ProductType Type { get; set; }
    //public string? Description { get; set; }
    public List<ProductPriceHistory> PriceHistory { get; set; } = new List<ProductPriceHistory>();
}

    
