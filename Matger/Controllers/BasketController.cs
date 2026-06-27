using Matger.Core.Entity;
using Matger.Core.Repository;
using Matger.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matger.Controllers
{
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId) //Get && ReCreate[to return empty basket same basketid 
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);

            //empty basket                 // Get Basket
            return basket is null ? new CustomerBasket(BasketId) : basket;
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket)
        {
            var CreateOrUpdateBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            if (CreateOrUpdateBasket == null) return BadRequest(new ApiResponse(400));

            return Ok(CreateOrUpdateBasket);
        }


        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
        {
            return await _basketRepository.DeleteBasketAsync(BasketId);
        }
    }
}
