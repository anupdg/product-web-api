using GalvProducts.Api.Data.Contracts;
using System.Threading.Tasks;

namespace GalvProducts.Api.Data
{
    /// <summary>
    /// Product repository
    /// </summary>
    public class ProductRepository : RepositoryBase<ProductEntity>, IProductRepository
    {
        public ProductRepository(ProductContext productContext) : base(productContext)
        {
        }
        public Task<int> SaveChanges()
        {
            return Context.SaveChangesAsync();
        }
    }
}
