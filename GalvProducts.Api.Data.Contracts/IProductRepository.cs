using System.Threading.Tasks;

namespace GalvProducts.Api.Data.Contracts
{
    /// <summary>
    /// Product repository interface
    /// </summary>
    public interface IProductRepository : IRepositoryBase<ProductEntity>
    {
        Task<int> SaveChanges();
    }
}
