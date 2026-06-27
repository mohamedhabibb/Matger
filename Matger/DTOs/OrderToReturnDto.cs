using Matger.Core.Entities.Order_Aggregate;

namespace Matger.DTOs
{
    public class OrderToReturnDto
    {

        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }

        public Address ShippingAddress { get; set; }
        public string DeliveryMethodName { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        // Navigational Property (Many)

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; }
    }
}
