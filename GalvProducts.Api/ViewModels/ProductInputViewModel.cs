using System;

namespace GalvProducts.Api
{
    /// <summary>
    /// Product input view model
    /// </summary>
    public class ProductInputViewModel
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Optional currency for rate conversion
        /// </summary>
        public string Currency { get; set; }
    }
}
