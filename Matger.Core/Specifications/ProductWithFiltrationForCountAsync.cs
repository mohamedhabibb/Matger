using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;

namespace Matger.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync : BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecPrams prams) :
            base(p =>
            (!prams.BrandId.HasValue || p.ProductBrandId == prams.BrandId)
            &&
            (!prams.TypeId.HasValue || p.ProductTypesId == prams.TypeId)
            )
        {

        }
    }
}
