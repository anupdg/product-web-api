using System;
using System.ComponentModel.DataAnnotations;

namespace GalvProducts.Api
{
    /// <summary>
    /// Product view model
    /// </summary>
    public class ProductCreateViewModel
    {
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
        [Range(1, 2000, ErrorMessage = "Please enter a price greater than >=1 and <=2000")]
        public float Price { get; set; }
    }

    public class ProductViewModel : ProductCreateViewModel
    {
        /// <summary>
        /// Product id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product view count
        /// </summary>
        public int ViewCount { get; set; } = 0;
    }
}
