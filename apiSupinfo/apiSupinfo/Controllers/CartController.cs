using System.Security.Claims;
using apiSupinfo.Models.Inputs.Product;
using apiSupinfo.Models.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class CartController : ControllerBase
{

    private readonly IConfiguration _config;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
        
        
    public CartController(IConfiguration config,ICartService service , IMapper mapper)
    {
        _config = config;
        _cartService = service;
        _mapper = mapper;
    }

    
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<Cart>> GetProducts()
    {
        var listOfU = _cartService.getCart(new User());
        if (listOfU == null)
            return NotFound(); 
        return Ok(listOfU);
    }
    
    
    [HttpPost("{id}")]
    [Authorize]
    public ActionResult<Cart> AddProduct(int id)
    {
        User currentUser = GetCurrentUser();
        CartCreateInput cart = new CartCreateInput
        {
            UserId = currentUser.Id,
            ProductId = id
        };
        var cartReceive = _cartService.addProduct(cart);
        return Ok(cartReceive);
    }
    
    
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Cart> DeleteProduct(int id)
    {
        User currentUser = GetCurrentUser();
        var product = _cartService.removeProduct(id, currentUser);
        return Ok(product);
    }
    
    private User GetCurrentUser()
    {
        if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            
        var userClaims = identity.Claims;
        return new User
        {
            Id = Int16.Parse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
            Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
            Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
        };
    }
}