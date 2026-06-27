using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities.Order_Aggregate;

namespace Matger.Core.Specifications
{
    public class OrderWithPaymentIntentSpacifications : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentSpacifications(string paymentIntentId) :base(o=>o.PaymentIntentId==paymentIntentId)
        {
            
        }
    }
}
