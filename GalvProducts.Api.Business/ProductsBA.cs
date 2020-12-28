using AutoMapper;
using GalvProducts.Api.Business.Contracts;
using GalvProducts.Api.Common;
using GalvProducts.Api.Data.Contracts;
using GalvProducts.Api.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalvProducts.Api.Business
{
    public class ProductsBA : IProductsBA
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICurrencyCacheService _currencyCacheService;
        public ProductsBA(IProductRepository productRepository, IMapper mapper, ICurrencyCacheService currencyCacheService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _currencyCacheService = currencyCacheService;
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productModel">Product model</param>
        /// <returns>Status</returns>
        public Task<int> CreateProduct(ProductModel productModel)
        {
            var entity = _mapper.Map<ProductEntity>(productModel);
            entity.Id = Guid.NewGuid();
            entity.ViewCount = 0;
            _productRepository.Create(entity);
            return _productRepository.SaveChanges();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="productInputModel">Product input model</param>
        /// <returns>delete status</returns>
        public Task<bool> DeleteProduct(ProductInputModel productInputModel)
        {
            var product = _productRepository.FindByFilter(c => c.Id.Equals(productInputModel.Id)).FirstOrDefault();
            if (product == null) {
                throw new GalvException("Woops! this product does not exist", StatusCodes.Status400BadRequest);
            }
            if (product.Deleted) {
                throw new GalvException("You are smart! Trying to delete this product again?", StatusCodes.Status400BadRequest);
            }
            product.Deleted = true;
            _productRepository.Update(product);
            _productRepository.SaveChanges();

            return Task.FromResult(true);
        }

        /// <summary>
        /// Get a single product
        /// </summary>
        /// <param name="productInputModel">Product input model</param>
        /// <returns>Single product</returns>
        public Task<ProductModel> GetProduct(ProductInputModel productInputModel)
        {
            var productEntity = _productRepository.FindByFilter(c => c.Id.Equals(productInputModel.Id)).FirstOrDefault();
            if (productEntity == null)
            {
                throw new GalvException("Woops! this product does not exist", StatusCodes.Status400BadRequest);
            }
            if (productEntity.Deleted)
            {
                throw new GalvException("You are smart! Trying to get a deleted product?", StatusCodes.Status400BadRequest);
            }
            var product = _mapper.Map<ProductModel>(productEntity);
            return Task.FromResult(product);
        }

        /// <summary>
        /// Get a single product and increase view count
        /// </summary>
        /// <param name="productInputModel">Product input model</param>
        /// <returns>Single product</returns>
        public Task<ProductModel> GetProductIncreaseViewcount(ProductInputModel productInputModel)
        {
            var productEntity = _productRepository.FindByFilter(c => c.Id.Equals(productInputModel.Id)).FirstOrDefault();
            if (productEntity == null)
            {
                throw new GalvException("Woops! this product does not exist", StatusCodes.Status400BadRequest);
            }
            if (productEntity.Deleted)
            {
                throw new GalvException("You are smart! Trying to get a deleted product?", StatusCodes.Status400BadRequest);
            }
            productEntity.ViewCount++;
            _productRepository.Update(productEntity);
            _productRepository.SaveChanges();

            var product = _mapper.Map<ProductModel>(productEntity);
            if (productInputModel.Currency != null)
            {
                var rate = _currencyCacheService.GetRate((CurrencyEnum)productInputModel.Currency);
                product.Price *= rate.Result;
            }
            return Task.FromResult(product);
        }

        /// <summary>
        /// List the most viewed products
        /// </summary>
        /// <param name="count">Count of products. Default to 5</param>
        /// <returns>Product list</returns>
        public Task<List<ProductModel>> GetProductsMostViewed(int count = 5)
        {
            var productsEntity = _productRepository.FindByFilter(c => c.ViewCount >= 1 && !c.Deleted).OrderByDescending(c => c.ViewCount).Take(count);
            var products = _mapper.Map<List<ProductModel>>(productsEntity);
            return Task.FromResult(products);
        }
    }
}
