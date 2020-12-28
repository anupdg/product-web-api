using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalvProducts.Api.Data.Contracts
{
    /// <summary>
    /// Database entity for product
    /// </summary>
    [Table("product")]
    public class ProductEntity
    {
        /// <summary>
        /// Product id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters")]
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
