using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entity;

namespace Matger.Core.Repository
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(String BasketId);

        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);

        public Task<bool> DeleteBasketAsync(string BasketId);
    }
}
