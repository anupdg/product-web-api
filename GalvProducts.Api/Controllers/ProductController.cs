using AutoMapper;
using GalvProducts.Api.Business.Contracts;
using GalvProducts.Api.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalvProducts.Api.Controllers
{
    /// <summary>
    /// Products APIs
    /// </summary>
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsBA _productsBA;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, IProductsBA productsBA, IMapper mapper)
        {
            _logger = logger;
            _productsBA = productsBA;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /product
        ///     {
        ///         "name": "Test product",
        ///         "description": "Test product description",
        ///         "price": 100
        ///     }
        /// </remarks>
        /// <param name="productCreateViewModel">New product details to create</param>
        /// <returns>Returns status of product creation</returns>
        [HttpPost()]
        public async Task<ActionResult<bool>> CreateProducts(ProductCreateViewModel productCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var input = _mapper.Map<ProductCreateViewModel, ProductModel>(productCreateViewModel);
                    var result = await _productsBA.CreateProduct(input);
                    return result > 0;
                }
                else
                {
                    throw new GalvException("Input is not valid", StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProducts", ex);
                throw new GalvException();
            }

        }

        /// <summary>
        /// Get product details by id. Optionally, return currency converted price for given currency
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///     POST /product
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "currency": "CAD"
        ///     }
        /// Sample response:
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "viewCount": 6,
        ///         "name": "Test",
        ///         "description": "New Data",
        ///         "price": 83.46651
        ///     }
        /// </remarks>
        /// <param name="productInputViewModel">Input model with product id and optional currency</param>
        /// <returns>Returns product details</returns>
        [HttpPost("detail")]
        public async Task<ActionResult<ProductViewModel>> GetProductDetails(ProductInputViewModel productInputViewModel)
        {
            if (productInputViewModel == null || productInputViewModel.Id.Equals(Guid.Empty))
            {
                throw new GalvException("Invalid product id", StatusCodes.Status400BadRequest);

            }
            var input = _mapper.Map<ProductInputModel>(productInputViewModel);
            var result = await _productsBA.GetProductIncreaseViewcount(input);
            if (result == null)
            {
                throw new GalvException("Product id not found", StatusCodes.Status404NotFound);
            }
            return Ok(_mapper.Map<ProductViewModel>(result));
        }

        /// <summary>
        /// Get most viewed products with minimum 1 view count
        /// </summary>
        /// Sample response:
        ///     [
        ///         {
        ///             "id": "85deb697-5861-4868-8ccd-544512b30a2d",
        ///             "viewCount": 6,
        ///             "name": "Test",
        ///             "description": "New Data",
        ///             "price": 65
        ///         },
        ///         {
        ///             "id": "831fe92b-1583-4b70-b472-3ab0ae62c68e",
        ///             "viewCount": 1,
        ///             "name": "Second product",
        ///             "description": "Second product description",
        ///            "price": 100
        ///        }
        ///     ]
        /// <param name="productCount">Optional product count</param>
        /// <returns>Most viewed products</returns>
        [HttpPost("mostviewed")]
        public async Task<ActionResult<List<ProductViewModel>>> GetMostViewed(int? productCount)
        {
            List<ProductViewModel> data;
            List<ProductModel> result;
            if (productCount > 0)
            {
                result = await _productsBA.GetProductsMostViewed((int)productCount);
            }
            else
            {
                result = await _productsBA.GetProductsMostViewed();
            }

            if (result == null)
            {
                throw new GalvException("No product data found", StatusCodes.Status404NotFound);
            }
            else
            {
                data = _mapper.Map<List<ProductModel>, List<ProductViewModel>>(result);
            }
            return Ok(data);
        }

        /// <summary>
        /// Delete a product by id
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>Success status</returns>
        [HttpDelete()]
        public async Task<ActionResult> DeleteProduct(Guid productId)
        {
            if (productId.Equals(Guid.Empty))
            {
                throw new GalvException("Invalid product id", StatusCodes.Status400BadRequest);
            }
            try
            {
                var result = await _productsBA.DeleteProduct(new ProductInputModel() { Id = productId });
                if (result)
                {
                    return Ok(true);
                }
                else
                {
                    throw new GalvException("No product data found", StatusCodes.Status400BadRequest);
                }
            }
            catch (GalvException ex)
            {
                throw ex;
            }
        }
    }
}
