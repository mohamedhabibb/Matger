using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;
using Matger.Core;
using Matger.Core.Entities.Order_Aggregate;
using Matger.Core.Repository;
using Matger.Core.Services;
using Matger.Core.Specifications.Order_Specifications;
using Matger.Core.Specifications;

namespace Matger.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        // private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork , IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //to create order you need
            // BuyerEmail ✅
            //ShippingAddress✅
            //DeliveryMethod from >>DeliveryMethodId
            //Items  >>> from basketid >>go to product 
            //SubTotal
            var basket = await _basketRepository.GetBasketAsync(basketId);
            var orderItems = new List<OrderItem>();

            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);  //get product by id that equal to basket.item.id
                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl); //ctor
                    var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
                    orderItems.Add(orderItem);
                }
            }

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);


            var Spec = new OrderWithPaymentIntentSpacifications(basket.PaymentIntentId);
            var ExpOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(Spec);

            if (ExpOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(ExpOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            var order = new Order(buyerEmail, ShippingAddress, deliveryMethod, orderItems, subTotal , basket.PaymentIntentId);

            await _unitOfWork.Repository<Order>().Add(order); //add local

            var Result = await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return DeliveryMethods;
        }

        public async Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail, int OrderId)
        {
            var Spec = new OrderSpecifications(buyerEmail, OrderId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(Spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var Spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
            return orders;
        }
    }
}
