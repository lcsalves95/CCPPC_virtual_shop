using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Enums;
using CCPPC.VirtualShop.Domain.Interfaces;
using CCPPC.VirtualShop.Domain.Models;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Application.Services
{
    public class VirtualStoreService : IVirtualStoreService
    {
        private readonly IVirtualStoreRepository _repository;
        private readonly LogModel _log;
        private readonly List<string> _messages;

        public VirtualStoreService(IVirtualStoreRepository repository)
        {
            _repository = repository;
            _log = new LogModel();
            _messages = new List<string>();
        }

        public async Task<RequestResult> AddItemOrder(long userId, ItemOrderViewModel model)
        {
            try
            {
                if (userId == default)
                {
                    _messages.Add("The [userId] field is required");
                    return new RequestResult(null, _messages, RequestStatus.Error);
                }

                ValidationResult validations = model.Validate();
                if (!validations.IsValid)
                {
                    _messages.AddRange(validations.Errors.Select(x => x.ErrorMessage));
                    return new RequestResult(null, _messages, RequestStatus.Error);
                }
                // verificar quantidade em stock antes
                Order order = await _repository.GetOpenOrder(userId);
                ItemOrder item;
                if (order != default)
                {
                    item = new ItemOrder(order.Id, model.ProductId, model.ProductName, model.Quantity, model.UnitaryValue);
                    order.AddItemOrder(item);
                    if (await _repository.AddItemOrder(item))
                    {
                        _messages.Add("Success");
                        return new RequestResult(order, _messages, RequestStatus.Success);
                    }
                    _log.Record(nameof(AddItemOrder),
                        $"An error occurred while trying to insert new item to this order. [order:{JsonConvert.SerializeObject(order)}]",
                        LogType.Error);
                    _messages.Add("An error occurred while trying to insert new item to this order");
                    return new RequestResult(null, _messages, RequestStatus.Error);
                }

                order = await _repository.AddOrder(new Order(userId));
                item = new ItemOrder(order.Id, model.ProductId, model.ProductName, model.Quantity, model.UnitaryValue);
                order.AddItemOrder(item);
                if (await _repository.AddItemOrder(item))
                {
                    _messages.Add("Success");
                    return new RequestResult(order, _messages, RequestStatus.Success);
                }
                _log.Record(nameof(AddItemOrder),
                    $"An error occurred while trying to insert new item to this order. [order:{JsonConvert.SerializeObject(order)}]",
                    LogType.Error);
                _messages.Add("An error occurred while trying to insert new item to this order");
                return new RequestResult(null, _messages, RequestStatus.Error);
            }
            catch (Exception ex)
            {
                _log.Record(nameof(AddItemOrder),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                _messages.Add("Ocorreu um erro inesperado ao tentar inserir o novo item.");
                return new RequestResult(null, _messages, RequestStatus.Error);
            }
        }

        public async Task<RequestResult> GetOpenOrder(long userId)
        {
            try
            {
                Order order = await _repository.GetOpenOrder(userId);
                if (order == default)
                {
                    _messages.Add("Not found an order for this user");
                    return new RequestResult(null, _messages, RequestStatus.NotFound);
                }
                _messages.Add("Success");
                return new RequestResult(order, _messages, RequestStatus.Success);
            }
            catch (Exception ex)
            {
                _log.Record(nameof(GetOpenOrder),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                _messages.Add("Ocorreu um erro inesperado ao tentar buscar o pedido.");
                return new RequestResult(null, _messages, RequestStatus.Error);
            }
        }
    }
}
