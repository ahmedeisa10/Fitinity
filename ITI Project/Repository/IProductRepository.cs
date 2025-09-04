using ITI_Project.Models;

namespace ITI_Project.Repository
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task DeleteProduct(Product product);
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
        Task UpdateProduct(Product product);
    }
}