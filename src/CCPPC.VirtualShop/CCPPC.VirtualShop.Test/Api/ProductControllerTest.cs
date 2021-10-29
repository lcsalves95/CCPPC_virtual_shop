using CCPPC.VirtualShop.Api.Controllers;
using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Enums;
using CCPPC.VirtualShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CCPPC.VirtualShop.Test.Api
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productService;
        private readonly ProductsController _controller;

        public ProductControllerTest()
        {
            _productService = new Mock<IProductService>();
            _controller = new ProductsController(_productService.Object);
        }

        [Fact]
        public async void Insert_ReturnShouldBeOk()
        {
            var requestResult = new RequestResult(true, new List<string> { "Success" }, RequestStatus.Success);

            _productService.Setup(x => x.Insert(It.IsAny<ProductViewModel>())).ReturnsAsync(requestResult);

            var response = await _controller.Insert(new ProductViewModel());

            Assert.NotNull(response);
            var objectResult = response as OkObjectResult;
            var content = objectResult.Value as RequestResult;

            Assert.NotNull(content);
            Assert.NotNull(content.Data);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(RequestStatus.Success, content.Status);

            _productService.Verify(x => x.Insert(It.IsAny<ProductViewModel>()), Times.Once);
        }

        [Fact]
        public async void Insert_WhenServiceReturnsError()
        {
            var requestResult = new RequestResult(null, new List<string> { "Error" }, RequestStatus.Error);

            _productService.Setup(x => x.Insert(It.IsAny<ProductViewModel>())).ReturnsAsync(requestResult);

            var response = await _controller.Insert(new ProductViewModel());
            Assert.NotNull(response);

            var objectResult = response as BadRequestObjectResult;
            var content = objectResult.Value as RequestResult;

            Assert.NotNull(content);
            Assert.Null(content.Data);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(RequestStatus.Error, content.Status);

            _productService.Verify(x => x.Insert(It.IsAny<ProductViewModel>()), Times.Once);
        }

        [Fact]
        public async void Update_ReturnShouldBeOk()
        {
            var requestResult = new RequestResult(true, new List<string> { "Success" }, RequestStatus.Success);

            _productService.Setup(x => x.Update(It.IsAny<long>(), It.IsAny<ProductViewModel>())).ReturnsAsync(requestResult);

            var response = await _controller.Update(It.IsAny<long>(), new ProductViewModel());

            Assert.NotNull(response);
            var objectResult = response as OkObjectResult;
            var content = objectResult.Value as RequestResult;

            Assert.NotNull(content);
            Assert.NotNull(content.Data);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(RequestStatus.Success, content.Status);

            _productService.Verify(x => x.Update(It.IsAny<long>(), It.IsAny<ProductViewModel>()), Times.Once);
        }

        [Fact]
        public async void Update_WhenServiceReturnsError()
        {
            var requestResult = new RequestResult(null, new List<string> { "Error" }, RequestStatus.Error);

            _productService.Setup(x => x.Update(It.IsAny<long>(), It.IsAny<ProductViewModel>())).ReturnsAsync(requestResult);

            var response = await _controller.Update(It.IsAny<long>(), new ProductViewModel());
            Assert.NotNull(response);

            var objectResult = response as BadRequestObjectResult;
            var content = objectResult.Value as RequestResult;

            Assert.NotNull(content);
            Assert.Null(content.Data);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(RequestStatus.Error, content.Status);

            _productService.Verify(x => x.Update(It.IsAny<long>(), It.IsAny<ProductViewModel>()), Times.Once);
        }
    }
}
