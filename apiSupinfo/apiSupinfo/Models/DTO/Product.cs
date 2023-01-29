using System.ComponentModel.DataAnnotations;

namespace ProjetWebAPI.Models.DTO;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}