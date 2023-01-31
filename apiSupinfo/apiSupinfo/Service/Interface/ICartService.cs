using apiSupinfo.Models.Inputs.Product;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Models.Service.Interface;

public interface ICartService
{
 public Cart addProduct(CartCreateInput cart);
 public Cart removeProduct(int id, User currentUser);
 public List<Cart> getCart(User currentUser);
}