using GrpcEF.Entites;
using Microsoft.EntityFrameworkCore;

namespace GrpcEF.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
    }
}
