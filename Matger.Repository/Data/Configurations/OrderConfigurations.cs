using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Matger.Core.Entities.Order_Aggregate;

namespace Matger.Repository.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status)                                         //Isert as string in DB
                 .HasConversion(o => o.ToString(), o=> (OrderStatus)Enum.Parse(typeof(OrderStatus), o));//return string(OrderStatus) from DB


            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");

            builder.OwnsOne(o => o.ShippingAddress, x => x.WithOwner());//Address Class not present in DB but exist in Order table


            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
