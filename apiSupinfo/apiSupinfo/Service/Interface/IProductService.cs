using apiSupinfo.Models.Inputs.Product;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Models.Service.Interface;

public interface IProductService
{
    public List<Product> GetProductsList();
    
    public Product GetProductById(int Id);
    
    public Product CreateProduct(Product input);
    
    public Product UpdateProduct(Product input, int idSeller);

    public Product DeleteProduct(int Id, int idSeller);
}