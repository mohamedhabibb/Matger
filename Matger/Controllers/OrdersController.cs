using Matger.Core.Services;
using Matger.Errors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Matger.DTOs;
using Matger.Core.Entities.Order_Aggregate;
using AutoMapper;

namespace Matger.Controllers
{
    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var mappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);


            //var address = new Address()
            //{
            //    FirstName = orderDto.ShippingAddress.FirstName,
            //    LastName = orderDto.ShippingAddress.LastName,
            //    City = orderDto.ShippingAddress.City,
            //    Street = orderDto.ShippingAddress.Street,
            //    Country = orderDto.ShippingAddress.Country,
            //};
            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, mappedAddress);

            if (order is null) return BadRequest(new ApiResponse(400, "There is a problem with your order"));

            var orderMapped = new OrderToReturnDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),   // map enum to string
                ShippingAddress = order.ShippingAddress,
                DeliveryMethodName = order.DeliveryMethod.ShortName,
                DeliveryMethodCost = order.DeliveryMethod.Cost,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    ProductId = oi.Product.ProductId,
                    ProductName = oi.Product.ProductName,
                    PictureUrl = oi.Product.PictureUrl,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),
                SubTotal = order.SubTotal,
                Total = order.Total,
                PaymentIntentId = order.PaymentIntentId
            };

            return Ok(orderMapped);


        }



        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForSpecificUserAsync(buyerEmail);

            if (orders is null)
                return NotFound(new ApiResponse(404, "No Orders found for this user"));

            var orderMapped = orders.Select(order => new OrderToReturnDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),   // map enum to string
                ShippingAddress = order.ShippingAddress,
                DeliveryMethodName = order.DeliveryMethod.ShortName,
                DeliveryMethodCost = order.DeliveryMethod.Cost,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    ProductId = oi.Product.ProductId,
                    ProductName = oi.Product.ProductName,
                    PictureUrl = oi.Product.PictureUrl,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),
                SubTotal = order.SubTotal,
                Total = order.Total,
                PaymentIntentId = order.PaymentIntentId
            }).ToList();

            return Ok(orderMapped);
        }




        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdForSpecificUserAsync(buyerEmail, id);

            if (order is null) return NotFound(new ApiResponse(404, $"No Order founded for this user's id = {id}"));

            var orderMapped = new OrderToReturnDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),   // map enum to string
                ShippingAddress = order.ShippingAddress,
                DeliveryMethodName = order.DeliveryMethod.ShortName,
                DeliveryMethodCost = order.DeliveryMethod.Cost,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    ProductId = oi.Product.ProductId,
                    ProductName = oi.Product.ProductName,
                    PictureUrl = oi.Product.PictureUrl,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),
                SubTotal = order.SubTotal,
                Total = order.Total,
                PaymentIntentId = order.PaymentIntentId
            };

            return Ok(orderMapped);


        }


        [HttpGet("DeliveryMethods")]

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
