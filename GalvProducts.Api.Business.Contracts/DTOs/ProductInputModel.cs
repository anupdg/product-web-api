using GalvProducts.Api.Common;
using System;

namespace GalvProducts.Api.Business.Contracts
{
    /// <summary>
    /// Product input business model
    /// </summary>
    public class ProductInputModel
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Optional currency for rate conversion
        /// </summary>
        public CurrencyEnum? Currency { get; set; }
    }
}
