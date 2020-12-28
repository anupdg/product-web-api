using AutoMapper;
using GalvProducts.Api.Business.Contracts;
using GalvProducts.Api.Common;
using GalvProducts.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalvProducts.Api.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        Mock<ILogger<ProductController>> logger;
        Mock<IProductsBA> productsBA;
        Mock<IMapper> mapper;
        string testProductName = "Test product";
        string testProductDescription = "Test product description";
        float testPrice = 100;

        [TestInitialize]
        public void Config()
        {
            logger = new Mock<ILogger<ProductController>>();
            productsBA = new Mock<IProductsBA>();
            mapper = new Mock<IMapper>();
        }

        #region Private members
        private ProductCreateViewModel GetProductSample()
        {
            return new ProductCreateViewModel()
            {
                Name = testProductName,
                Description = testProductDescription,
                Price = testPrice
            };
        }
        private ProductModel GetProductSampleBA()
        {
            return new ProductModel()
            {
                Name = testProductName,
                Description = testProductDescription,
                Price = testPrice
            };
        }
        #endregion

        [TestMethod]
        public async Task CreateProducts_success()
        {

            //Setup
            ProductCreateViewModel productCreateViewModel = GetProductSample();
            ProductModel productModel = GetProductSampleBA();
            mapper.Setup(m => m.Map<ProductCreateViewModel, ProductModel>(productCreateViewModel)).Returns(productModel);
            productsBA.Setup(repo => repo.CreateProduct(productModel)).Returns(Task.FromResult(1));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.CreateProducts(productCreateViewModel);

            //Assert
            Assert.AreEqual(result.Value, true);
        }

        [TestMethod]
        public async Task CreateProducts_Failure()
        {

            //Setup
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel();
            ProductModel productModel = GetProductSampleBA();
            mapper.Setup(m => m.Map<ProductCreateViewModel, ProductModel>(productCreateViewModel)).Returns(productModel);
            productsBA.Setup(repo => repo.CreateProduct(productModel)).Returns(Task.FromResult(0));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.CreateProducts(productCreateViewModel);

            //Assert
            Assert.AreEqual(result.Value, false);
        }

        [TestMethod]
        public void CreateProducts_Name_Required()
        {
            //Setup
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel()
            {
                Description = testProductDescription,
                Price = testPrice
            };

            //Action
            var result = ValidationHelper.Validate(productCreateViewModel);

            //Assert
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual("Name is required", result[0].ErrorMessage);
        }

        [TestMethod]
        public void CreateProducts_Name_Length()
        {
            //Setup
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel()
            {
                Name = "Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name Long name ",
                Description = testProductDescription,
                Price = testPrice
            };

            //Action
            var result = ValidationHelper.Validate(productCreateViewModel);

            //Assert
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual("Name can't be longer than 100 characters", result[0].ErrorMessage);
        }

        [TestMethod]
        public void CreateProducts_Description_Length()
        {
            //Setup
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel()
            {
                Name = testProductName,
                Description = "Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description Large description ",
                Price = testPrice
            };

            //Action
            var result = ValidationHelper.Validate(productCreateViewModel);

            //Assert
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual("Description can't be longer than 1000 characters", result[0].ErrorMessage);
        }

        [TestMethod]
        public void CreateProducts_Valid_Price()
        {
            //Setup
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel()
            {
                Name = testProductName,
                Description = testProductDescription,
                Price = 0
            };

            //Action
            var result = ValidationHelper.Validate(productCreateViewModel);

            //Assert
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual("Please enter a price greater than >=1 and <=2000", result[0].ErrorMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(GalvException))]
        public async Task GetProductDetails_Input_Null()
        {
            //Setup
            ProductInputViewModel productInputViewModel = null;

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.GetProductDetails(productInputViewModel);
        }

        [TestMethod]
        [ExpectedException(typeof(GalvException))]
        public async Task GetProductDetails_Input_ProductId_Empty()
        {
            //Setup
            ProductInputViewModel productInputViewModel = new ProductInputViewModel() { Id = Guid.Empty };

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.GetProductDetails(productInputViewModel);
        }

        [TestMethod]
        [ExpectedException(typeof(GalvException))]
        public async Task GetProductDetails_Does_not_exist()
        {
            //Setup
            var productId = Guid.NewGuid();
            ProductModel data = null;
            ProductInputViewModel productInputViewModel = new ProductInputViewModel() { Id = productId };
            ProductInputModel productInputModel = new ProductInputModel() { Id = productId };
            mapper.Setup(m => m.Map<ProductInputModel>(productInputViewModel)).Returns(productInputModel);
            productsBA.Setup(repo => repo.GetProductIncreaseViewcount(productInputModel)).Returns(Task.FromResult(data));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.GetProductDetails(productInputViewModel);
        }

        [TestMethod]
        public async Task GetProductDetails_Success()
        {
            //Setup
            var productId = Guid.NewGuid();
            ProductModel data = new ProductModel() { Id = productId };
            ProductViewModel resultProduct = new ProductViewModel() { Id = productId };
            ProductInputViewModel productInputViewModel = new ProductInputViewModel() { Id = productId };
            ProductInputModel productInputModel = new ProductInputModel() { Id = productId };
            mapper.Setup(m => m.Map<ProductInputModel>(productInputViewModel)).Returns(productInputModel);
            mapper.Setup(m => m.Map<ProductViewModel>(data)).Returns(resultProduct);
            productsBA.Setup(repo => repo.GetProductIncreaseViewcount(productInputModel)).Returns(Task.FromResult(data));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.GetProductDetails(productInputViewModel);

            //Assert
            var okObjectResult = result.Result as OkObjectResult;
            var product = okObjectResult.Value as ProductViewModel;
            Assert.AreEqual(product.Id, productId);
        }

        [TestMethod]
        [ExpectedException(typeof(GalvException))]
        public async Task GetMostViewed_NoData()
        {
            //Setup
            List<ProductModel> result = null;
            productsBA.Setup(repo => repo.GetProductsMostViewed(It.IsAny<int>())).Returns(Task.FromResult(result));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            await controller.GetMostViewed(1);
        }

        [TestMethod]
        public async Task GetMostViewed_Success()
        {
            //Setup
            var productId = Guid.NewGuid();
            List<ProductModel> resultBa = new List<ProductModel> { new ProductModel() { Id = productId } };
            List<ProductViewModel> result = new List<ProductViewModel> { new ProductViewModel() { Id = productId } };
            mapper.Setup(m => m.Map<List<ProductModel>, List<ProductViewModel>>(It.IsAny< List<ProductModel>>())).Returns(result);
            productsBA.Setup(repo => repo.GetProductsMostViewed(It.IsAny<int>())).Returns(Task.FromResult(resultBa));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var resultData = await controller.GetMostViewed(1);

            //assert
            var okObjectResult = resultData.Result as OkObjectResult;
            var products = okObjectResult.Value as List<ProductViewModel>;
            Assert.AreEqual(products.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(GalvException))]
        public async Task DeleteProduct_Invalid_Input()
        {
            //Setup

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            await controller.DeleteProduct(Guid.Empty);
        }


        [TestMethod]
        [ExpectedException(typeof(GalvException))]
        public async Task DeleteProduct_Product_Does_Not_Exist()
        {
            //Setup
            productsBA.Setup(repo => repo.DeleteProduct(It.IsAny<ProductInputModel>())).Returns(Task.FromResult(false));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.DeleteProduct(Guid.NewGuid());

            //Assert
        }

        [TestMethod]
        public async Task DeleteProduct_Success()
        {
            //Setup
            productsBA.Setup(repo => repo.DeleteProduct(It.IsAny<ProductInputModel>())).Returns(Task.FromResult(true));

            //Action
            var controller = new ProductController(logger.Object, productsBA.Object, mapper.Object);
            var result = await controller.DeleteProduct(Guid.NewGuid());

            //Assert
            var ok = result as OkObjectResult;
            Assert.AreEqual(true, ok.Value);
        }
    }
}
