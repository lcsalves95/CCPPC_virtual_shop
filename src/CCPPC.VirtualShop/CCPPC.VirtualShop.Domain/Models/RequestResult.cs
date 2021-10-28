using CCPPC.VirtualShop.Domain.Enums;
using FluentValidation.Results;
using System.Collections.Generic;

namespace CCPPC.VirtualShop.Domain.Models
{
    public class RequestResult
    {
        public RequestResult(object data, IList<string> messages, RequestStatus status)
        {
            Data = data;
            Messages = messages;
            Status = status;
        }

        public object Data { get; set; }
        public IList<string> Messages { get; set; }
        public RequestStatus Status { get; set; }
    }
}
