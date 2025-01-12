using ManageProductsAPI.Models;

namespace ManageProductsAPI.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> DecrementStockAsync(int id, int quantity);
        Task<bool> AddToStockAsync(int id, int quantity);
    }
}
