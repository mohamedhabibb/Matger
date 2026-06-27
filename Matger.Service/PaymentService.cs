using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core;
using Matger.Core.Entities;
using Matger.Core.Entities.Order_Aggregate;
using Matger.Core.Entity;
using Matger.Core.Repository;
using Matger.Core.Services;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Matger.Core.Entities.Product;


namespace Matger.Service
{
    public class PaymentService : IPaymentService
    {
        
             private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];

            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket is null) return null;

            var SheppingPrice = 0m;

            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value);
                SheppingPrice = DeliveryMethod.Cost;
            }

            if (Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if (item.Price != Product.Price)
                        item.Price = Product.Price;

                }
            }


            var SubTotal = Basket.Items.Sum(item => item.Price * item.Quantity);


            //Create payment intent service
            PaymentIntent paymentIntent;
            var Service = new PaymentIntentService();
            if (string.IsNullOrEmpty(Basket.PaymentIntentId))//create
            {
                var Option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)SheppingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await Service.CreateAsync(Option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else//update
            {
                var Option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)SheppingPrice * 100
                };
                paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId, Option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }

            await _basketRepository.UpdateBasketAsync(Basket);

            return Basket;
        }
    }
}

