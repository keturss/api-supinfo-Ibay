using System.ComponentModel.DataAnnotations;

namespace ProjetWebAPI.Models.DTO;

public class Cart
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}