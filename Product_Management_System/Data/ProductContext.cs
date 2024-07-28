using Microsoft.EntityFrameworkCore;

namespace Product_Management_System.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<ProductDTO> Products { get; set; }
    }
}
