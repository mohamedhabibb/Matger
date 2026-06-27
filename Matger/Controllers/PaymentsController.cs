using Matger.Core.Services;
using Matger.DTOs;
using Matger.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matger.Controllers
{
    [Authorize]
    public class PaymentsController : APIBaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var customerBasket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (customerBasket is null) return BadRequest(new ApiResponse(400, "Problem in your Basket Payment"));

            CustomerBasketDto customerBasketDto = new CustomerBasketDto()
            {
                Id = customerBasket.Id,
                DeliveryMethodId = customerBasket.DeliveryMethodId,
                PaymentIntentId = customerBasket.PaymentIntentId,
                ClientSecret = customerBasket.ClientSecret,
                Items = customerBasket.Items.Select(item => new BasketItemDto
                {
                    Id = item.Id,
                    Brand = item.Brand,
                    Type = item.Type,
                    PictureUrl = item.PictureUrl,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            return Ok(customerBasketDto);
        }

    }
}
