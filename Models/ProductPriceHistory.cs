using System.ComponentModel.DataAnnotations;

namespace Stx.Models;

public class ProductPriceHistory
{
    public long Id { get; set; }
    public DateTime ValidFrom { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    //[MaxLength(7)]
    //public string? Code { get; set; }
    //public string? Name { get; set; }
    //public bool? Active { get; set; }
    public double? Price { get; set; }
    public double? PriceChange { get; set; }
    public int? PriceChangeInt { get; set; }
    //public double? PrevPrice { get; set; }
    //public DateTime? ReleaseDate { get; set; }
    //public int? Rating { get; set; }
    //public ProductType Type { get; set; }
    //public string? Description { get; set; }
}


