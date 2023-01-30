using System.ComponentModel.DataAnnotations;

namespace ProjetWebAPI.Models.DTO;

public class Cart
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
}
