using apiSupinfo.Models.Inputs.Product;
using apiSupinfo.Models.Service.Interface;
using AutoMapper;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Models.Service;

public class ProductService : IProductService
{
    
    private DbFactoryContext _context;
    private readonly IMapper _mapper;

    public ProductService(DbFactoryContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<Product> GetProductsList()
    {
        List<Product> ProductList;
        try
        {
            ProductList = _context.Set<Product>().ToList();
        }
        catch (Exception)
        {
            throw;
        }
        return ProductList;
    }

    public Product GetProductById(int Id)
    {
        Product product;
        try
        {
            product = _context.Find<Product>(Id);
        }
        catch (Exception)
        {
            throw;
        }
        return product;
    }

    public Product CreateProduct(Product input)
    {
        var product = _mapper.Map<Product>(input);
        _context.Add<Product>(product);

        _context.SaveChanges();
        return product;
    }

    public Product UpdateProduct(Product input, int idSeller)
    {
        var product = _mapper.Map<Product>(input);
        Product TempProduct=GetProductById(product.Id);
        
        if (product.SellerId != idSeller) return product;   //TODO error message
        
        if (TempProduct != null)
        {
            TempProduct.Name = product.Name;
                
            _context.Update<Product>(TempProduct);
        }   
           
        _context.SaveChanges();
        return TempProduct;
    }

    public Product DeleteProduct(int Id,int idSeller)
    {
        var product=GetProductById(Id);
            
        if (product == null) return product;//TODO error message
        if (product.SellerId != idSeller) return product;//TODO error message
            
        _context.Remove<Product>(product);
        _context.SaveChanges();
            
        return product;
    }
    
    
    private bool ProductExists(string name)
    {
        return _context.Products.Any(e => e.Name == name);
    }
}