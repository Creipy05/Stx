namespace Stx.Models;

public class Product
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public bool? Active { get; set; }
    public double? Price { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? Rating { get; set; }
    public ProductType Type { get; set; }
    public string? Description { get; set; }
    public List<ProductHistory> History { get; set; } = new List<ProductHistory>();
}

    
