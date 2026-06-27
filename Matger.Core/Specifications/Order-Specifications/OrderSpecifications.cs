using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities.Order_Aggregate;

namespace Matger.Core.Specifications.Order_Specifications
{
    public class OrderSpecifications :BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail):base(o=>o.BuyerEmail== buyerEmail) 
        {
            Includes.Add(d => d.DeliveryMethod);
            Includes.Add(oi => oi.Items);
            AddOrderByDescending(o => o.OrderDate);
            
        }

        public OrderSpecifications(string buyerEmail , int OderId) : base(o => o.BuyerEmail == buyerEmail && o.Id== OderId)
        {
            Includes.Add(d => d.DeliveryMethod);
            Includes.Add(oi => oi.Items);
        }
    }
}
