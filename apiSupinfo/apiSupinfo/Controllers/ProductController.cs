using System.Security.Claims;
using apiSupinfo.Models.Inputs.Product;
using apiSupinfo.Models.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
        
        
    public ProductController(IConfiguration config,IProductService service , IMapper mapper)
    {
        _config = config;
        _productService = service;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<Product> GetProducts()
    {
        var listOfU =  _productService.GetProductsList();
        var productViewM = _mapper.Map<List<Product>>(listOfU);
        if (listOfU == null)
            return NotFound(); 
        return Ok(productViewM);
    }


    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _productService.GetProductById(id);
        var productViewM = _mapper.Map<Product>(product);
        if (product == null)
            return NotFound();
        return Ok(productViewM);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Seller, Admin")]
    public ActionResult<Product> UpdateProduct(int id,[FromForm] ProductUpdateInput input)
    {
        var product = _mapper.Map<Product>(input);
        
        if (id != product.Id)
        {
            return BadRequest();
        }
        
        var currentUser = GetCurrentUser();

        var productTemp = _productService.UpdateProduct(product, currentUser.Id);

        return Ok(productTemp);
    }

    
    [HttpPost]
    [Authorize(Roles = "Seller, Admin")]
    public ActionResult<Product> CreateProduct([FromForm] ProductCreateInput input)
    {
        var product = _mapper.Map<Product>(input);
        
        var currentUser = GetCurrentUser();
        product.SellerId = currentUser.Id;
        
        var productTemp = _productService.CreateProduct(product); //TODO if exist
        return Ok(productTemp);
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Seller, Admin")]
    public ActionResult<Product> DeleteProduct(int id)
    {
        var currentUser = GetCurrentUser();
        var product = _productService.DeleteProduct(id, currentUser.Id);
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