namespace apiSupinfo.Models.Inputs.Product;

public class ProductUpdateInput
{
    public int? SellerId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}