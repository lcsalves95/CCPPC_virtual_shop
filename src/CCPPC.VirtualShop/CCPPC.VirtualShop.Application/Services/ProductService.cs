using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Enums;
using CCPPC.VirtualShop.Domain.Interfaces;
using CCPPC.VirtualShop.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly List<string> _messages;
        private readonly LogModel _log;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
            _messages = new List<string>();
            _log = new LogModel();
        }

        public async Task<RequestResult> Get(long productId)
        {
            try
            {
                if (productId <= 0)
                {
                    _messages.Add("The [productId] field is required");
                    return new RequestResult(null, _messages, RequestStatus.Error);
                }

                Product product = await _repository.Get(productId);
                if (product == default)
                {
                    _messages.Add("Not found a product with the informed ID");
                    return new RequestResult(null, _messages, RequestStatus.NotFound);
                }

                _messages.Add("Success");
                return new RequestResult(product, _messages, RequestStatus.Success);
            }
            catch (Exception ex)
            {
                _log.Record(nameof(Get),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                _messages.Add("Ocorreu um erro inesperado ao realizar o Get.");
                return new RequestResult(null, _messages, RequestStatus.Error);
            }
        }

        public async Task<RequestResult> GetAll()
        {
            try
            {
                IEnumerable<Product> products = await _repository.GetAll();
                return new RequestResult(products, _messages, RequestStatus.Success);
            }
            catch (Exception ex)
            {
                _log.Record(nameof(GetAll),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                _messages.Add("Ocorreu um erro inesperado ao realizar o GetAll.");
                return new RequestResult(null, _messages, RequestStatus.Error);
            }
        }

        public async Task<RequestResult> Insert(ProductViewModel model)
        {
            try
            {
                ValidationResult validations = model.Validate();
                if (!validations.IsValid)
                {
                    _messages.AddRange(validations.Errors.Select(x => x.ErrorMessage));
                    return new RequestResult(null, _messages, RequestStatus.Error);
                }

                var productExists = await _repository.GetByName(model.Name);
                if (productExists != default)
                {
                    productExists.AddStockQuantity(model.Quantity);
                    productExists = await _repository.Update(productExists);

                    _messages.Add("Success");
                    return new RequestResult(productExists, _messages, RequestStatus.Success);
                }

                Product product = await _repository.Insert(new Product(model.Name, model.Description, model.Quantity, model.Value));
                _messages.Add("Success");
                return new RequestResult(product, _messages, RequestStatus.Success);
            }
            catch (Exception ex)
            {
                _log.Record(nameof(Insert),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                _messages.Add("Ocorreu um erro inesperado ao realizar o Insert.");
                return new RequestResult(null, _messages, RequestStatus.Error);
            }
        }

        public async Task<RequestResult> Update(long productId, ProductViewModel model)
        {
            try
            {
                if (productId <= 0)
                {
                    _messages.Add("The [productId] field is required");
                    return new RequestResult(null, _messages, RequestStatus.Error);
                }

                ValidationResult validations = model.Validate();
                if (!validations.IsValid)
                {
                    _messages.AddRange(validations.Errors.Select(x => x.ErrorMessage));
                    return new RequestResult(null, _messages, RequestStatus.Error);
                }

                Product product = await _repository.Get(productId);
                if (product == default)
                {
                    _messages.Add("Not found a product with the informed ID");
                    return new RequestResult(null, _messages, RequestStatus.NotFound);
                }

                product.UpdateName(model.Name);
                product.UpdateDescription(model.Description);
                product.UpdateValue(model.Value);
                product.UpdateStockQuantity(model.Quantity);

                product = await _repository.Update(product);
                _messages.Add("Success");
                return new RequestResult(product, _messages, RequestStatus.Success);
            }
            catch (Exception ex)
            {
                _log.Record(nameof(Update),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                _messages.Add("Ocorreu um erro inesperado ao realizar o Update.");
                return new RequestResult(null, _messages, RequestStatus.Error);
            }
        }
    }
}
