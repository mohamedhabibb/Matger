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
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(oi => oi.Product , p=>p.WithOwner()); // FOR THIS >>public ProductItemOrdered Product { get; set; } TO ADD IN ORDERITEM TABLE ONLY
            builder.Property(oi => oi.Price)
               .HasColumnType("decimal(18,2)");
        }
    }
}
