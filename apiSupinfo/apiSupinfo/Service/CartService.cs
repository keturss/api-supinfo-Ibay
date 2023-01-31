using System.Runtime.Intrinsics.X86;
using apiSupinfo.Models.Inputs.Product;
using apiSupinfo.Models.Service.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Models.Service;

public class CartService : ICartService
{
    private DbFactoryContext _context;
    
    private readonly IMapper _mapper;

    public CartService(DbFactoryContext context,IMapper mapper)
    {
        _context = context;
        
        _mapper = mapper;
    }

    public Cart addProduct(CartCreateInput input)
    {
        var cart = _mapper.Map<Cart>(input);
        _context.Add<Cart>(cart);

        _context.SaveChanges();
        return cart;
    }
    

    public Cart removeProduct(int id, User currentUser)
    {

        var cart=GetCartById(id);

        if (currentUser.Id != cart.UserId) return cart; 
        if (cart == null) return cart;//TODO error message
            
        _context.Remove<Cart>(cart);
        _context.SaveChanges();
            
        return cart;
    }

    private Cart GetCartById(int id)
    {
        Cart product;
        try
        {
            product = _context.Find<Cart>(id);
        }
        catch (Exception)
        {
            throw;
        }
        return product;
    }
    
    public List<Cart> getCart(User currentUser)
    {
        List<Cart> CartList;
        try
        {
            CartList = _context.Set<Cart>().ToList();
        }
        catch (Exception)
        {
            throw;
        }
        return CartList;
    }
}