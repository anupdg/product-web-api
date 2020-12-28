using System;

namespace GalvProducts.Api.Business.Contracts
{
    /// <summary>
    /// Product business model
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Product id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Product view count
        /// </summary>
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// Logical deleted flag
        /// </summary>
        public bool Deleted { get; set; }
    }
}
