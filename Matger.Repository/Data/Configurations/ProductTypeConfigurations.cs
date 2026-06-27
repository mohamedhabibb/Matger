using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Talabat.Repository.Data.Configurations
{
    internal class ProductTypeConfigurations : IEntityTypeConfiguration<ProductTypes>
    {
        public void Configure(EntityTypeBuilder<ProductTypes> builder)
        {
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
