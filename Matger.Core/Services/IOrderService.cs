using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities.Order_Aggregate;

namespace Matger.Core.Services
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId, Address ShippingAddress);

        public Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail, int OrderId);

        public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string buyerEmail);

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
