using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Application.Services;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Enums;
using CCPPC.VirtualShop.Domain.Interfaces;
using Moq;
using System;
using Xunit;

namespace CCPPC.VirtualShop.Test.Application
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _repository;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _repository = new Mock<IProductRepository>();
            _service = new ProductService(_repository.Object);
        }

        [Fact]
        public async void Insert_ReturnShouldBeOk()
        {
            _repository.Setup(x => x.Insert(It.IsAny<Product>())).ReturnsAsync(new Product("Teste", "Teste", 10, 2.99));
            _repository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(default(Product));

            var response = await _service.Insert(GetViewModel(true));

            Assert.NotNull(response);
            Assert.NotNull(response.Data);

            var product = response.Data as Product;
            Assert.NotNull(product);
            Assert.NotEmpty(product.Name);

            Assert.NotEmpty(response.Messages);
            Assert.Equal(RequestStatus.Success, response.Status);

            _repository.Verify(x => x.Insert(It.IsAny<Product>()), Times.Once);
            _repository.Verify(x => x.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void Insert_WhenModelIsInvalid()
        {
            var response = await _service.Insert(GetViewModel(false));

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEmpty(response.Messages);
            Assert.Contains("The [Name] field is required", response.Messages);
            Assert.Contains("The [Description] field is required", response.Messages);
            Assert.Contains("The [Value] field must be valid", response.Messages);
            Assert.Contains("The [Quantity] field must be valid", response.Messages);
            Assert.Equal(RequestStatus.Error, response.Status);
        }

        [Fact]
        public async void Insert_ReturnShouldBeOk_WhenGetByNameReturnOk()
        {
            _repository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(new Product("Teste", "Teste", 10, 2.99));
            _repository.Setup(x => x.Update(It.IsAny<Product>())).ReturnsAsync(new Product("Teste Att", "Teste Att", 10, 2.99));

            var response = await _service.Insert(GetViewModel(true));

            Assert.NotNull(response);
            Assert.NotNull(response.Data);

            var product = response.Data as Product;
            Assert.NotNull(product);
            Assert.NotEmpty(product.Name);

            Assert.NotEmpty(response.Messages);
            Assert.Equal(RequestStatus.Success, response.Status);

            _repository.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
            _repository.Verify(x => x.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void Insert_WhenRepositoryThrowsException()
        {
            _repository.Setup(x => x.GetByName(It.IsAny<string>())).ThrowsAsync(new Exception());

            var response = await _service.Insert(GetViewModel(true));

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEmpty(response.Messages);
            Assert.Contains("Ocorreu um erro inesperado ao realizar o Insert.", response.Messages);
            Assert.Equal(RequestStatus.Error, response.Status);
        }

        [Fact]
        public async void Update_ReturnShouldBeOk()
        {
            _repository.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(new Product("Teste", "Teste", 10, 2.99));
            _repository.Setup(x => x.Update(It.IsAny<Product>())).ReturnsAsync(new Product("Teste Att", "Teste Att", 10, 2.99));

            var response = await _service.Update(123, GetViewModel(true));

            Assert.NotNull(response);
            Assert.NotNull(response.Data);

            var product = response.Data as Product;
            Assert.NotNull(product);
            Assert.NotEmpty(product.Name);

            Assert.NotEmpty(response.Messages);
            Assert.Equal(RequestStatus.Success, response.Status);

            _repository.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
            _repository.Verify(x => x.Get(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Update_WhenProductIdIsInvalid()
        {

            var response = await _service.Update(default(long), GetViewModel(true));

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEmpty(response.Messages);
            Assert.Contains("The [productId] field is required", response.Messages);
            Assert.Equal(RequestStatus.Error, response.Status);
        }

        [Fact]
        public async void Update_WhenProductNotFound()
        {
            _repository.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(default(Product));

            var response = await _service.Update(123, GetViewModel(true));

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEmpty(response.Messages);
            Assert.Contains("Not found a product with the informed ID", response.Messages);
            Assert.Equal(RequestStatus.NotFound, response.Status);

            _repository.Verify(x => x.Get(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async void Update_WhenRepositoryThrowsException()
        {
            _repository.Setup(x => x.Get(It.IsAny<long>())).ThrowsAsync(new Exception());

            var response = await _service.Update(123, GetViewModel(true));

            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.NotEmpty(response.Messages);
            Assert.Contains("Ocorreu um erro inesperado ao realizar o Update.", response.Messages);
            Assert.Equal(RequestStatus.Error, response.Status);
        }

        private ProductViewModel GetViewModel(bool valid = true)
        {
            if (valid)
                return new ProductViewModel() { Name = "Teste de Unidade", Description = "Teste de Unidade", Quantity = 10, Value = 2.99 };
            return new ProductViewModel() { Name = "", Description = "", Quantity = 0, Value = 0 };
        }
    }
}
