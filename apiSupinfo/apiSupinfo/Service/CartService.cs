using System.Runtime.Intrinsics.X86;
using apiSupinfo.Models.Service.Interface;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Models.Service;

public class CartService : ICartService
{
    private DbFactoryContext _context;

    public CartService(DbFactoryContext context)
    {
        _context = context;
    }

    public Cart? addProduct(int id, User user)
    {
        var item = _context.Carts.FromSql($"INSERT INTO Carts Values '{user.Id}','{id}';").ToList();
        return item != null ? (Cart)item[0] : null;
    }
    

    public Cart removeProduct(int id, User currentUser)
    {
        var item = _context.Carts.FromSql($"Delete From Carts Where ProductId = {id} AND Id = {currentUser.Id};").ToList();
        return item != null ? (Cart)item[0] : null;
    }

    private Cart GetCartById(int id, User currentUser)
    {
        var item = _context.Carts.FromSql($"SELECT * From Carts Where ProductId = {id} AND Id = {currentUser.Id};").ToList();
        return item != null ? (Cart)item[0] : null;
    }
    
    public List<Cart> getCart(User currentUser)
    {
        var cart = _context.Carts.FromSql($"SELECT * From Carts Where UserId = {currentUser.Id};").ToList();
        return cart;
    }
}