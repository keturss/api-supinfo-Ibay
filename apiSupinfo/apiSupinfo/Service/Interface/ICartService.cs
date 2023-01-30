using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Models.Service.Interface;

public interface ICartService
{
 public Cart addProduct(int id, User currentUser);
 public Cart removeProduct(int id, User currentUser);
 public List<Cart> getCart(User currentUser);
}