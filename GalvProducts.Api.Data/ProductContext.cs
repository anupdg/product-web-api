using GalvProducts.Api.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GalvProducts.Api.Data
{
    /// <summary>
    /// Product DB context
    /// </summary>
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProductEntity> Products { get; set; }
    }
}
