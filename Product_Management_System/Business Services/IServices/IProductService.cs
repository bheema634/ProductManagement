using Product_Management_System.Models;

namespace Product_Management_System.Business_Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task<int> GetTotalCount();
        Task<IEnumerable<Product>> SortProducts(string sortBy, bool ascending);
        Task AddProduct(Product product);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
        Task DeleteAllProducts();
    }

}
