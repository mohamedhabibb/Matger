using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;

namespace Matger.Core.Specifications
{
    public class ProductWithrBrandandTypeSpecifications : BaseSpecifications<Product>
    {
        public ProductWithrBrandandTypeSpecifications(ProductSpecPrams prams)
            : base(p =>
            (string.IsNullOrEmpty(prams.SearchName) || p.Name.ToLower().Contains(prams.SearchName.ToLower()))
            &&
            (!prams.BrandId.HasValue || p.ProductBrandId==prams.BrandId)  
            &&
            (!prams.TypeId.HasValue || p.ProductTypesId == prams.TypeId)
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p=>p.ProductTypes);

            //EXAMPLE
            //praducts= 100
            //pagesize=10
            //pageindex = 5
            ApplyPagination(prams.PageSize * (prams.PageIndex - 1), prams.PageSize);

            if (!string.IsNullOrEmpty(prams.sort))
            {
                switch (prams.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithrBrandandTypeSpecifications(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductTypes);

        }
    }
}
