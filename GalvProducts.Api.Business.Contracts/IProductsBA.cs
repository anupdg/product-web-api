using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalvProducts.Api.Business.Contracts
{
    public interface IProductsBA
    {
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productModel">Product model</param>
        /// <returns>Status</returns>
        Task<int> CreateProduct(ProductModel productModel);


        /// <summary>
        /// Get a single product
        /// </summary>
        /// <param name="productInputModel">Product input model</param>
        /// <returns>Single product</returns>
        Task<ProductModel> GetProduct(ProductInputModel productInputModel);

        /// <summary>
        /// Get a single product and increase view count
        /// </summary>
        /// <param name="productInputModel">Product input model</param>
        /// <returns>Single product</returns>
        Task<ProductModel> GetProductIncreaseViewcount(ProductInputModel productInputModel);

        /// <summary>
        /// List the most viewed products
        /// </summary>
        /// <param name="count">Count of products. Default to 5</param>
        /// <returns></returns>
        Task<List<ProductModel>> GetProductsMostViewed(int count = 5);

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="productInputModel">Product input model</param>
        /// <returns>delete status</returns>
        Task<bool> DeleteProduct(ProductInputModel productInputModel);
    }
}
