using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Matger.Core.Entities;
using Matger.Core.Entities.Order_Aggregate;

namespace Matger.Repository.Data
{
    public class StoreContextSeed
    {

        public static async Task SeedAsynk(StoreContext storDbcontrext)
        {
            if (!storDbcontrext.ProductBrands.Any())
            {
                #region seeding Brands
                var BrandData = File.ReadAllText("../Matger.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (brands?.Count > 0)
                {
                    foreach (var item in brands)

                    {
                        await storDbcontrext.Set<ProductBrand>().AddAsync(item);
                    }
                    await storDbcontrext.SaveChangesAsync();

                }
                #endregion

            }
            if (!storDbcontrext.ProductTypes.Any())
            {
                #region Seeding Types
                var TypeData = File.ReadAllText("../Matger.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductTypes>>(TypeData);

                if (Types?.Count > 0)
                {
                    foreach (var item in Types)

                    {
                        await storDbcontrext.Set<ProductTypes>().AddAsync(item);
                    }
                    await storDbcontrext.SaveChangesAsync();

                }
                #endregion
            }

            if (!storDbcontrext.Products.Any())
            {

                #region Seeding Product
                var ProductData = File.ReadAllText("../Matger.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (Products?.Count > 0)
                {
                    foreach (var item in Products)

                    {
                        await storDbcontrext.Set<Product>().AddAsync(item);
                    }
                    await storDbcontrext.SaveChangesAsync();

                }

                #endregion

            }


            if (!storDbcontrext.DeliveryMethods.Any())
            {
                #region Seeding deliveryMethod
                var deliveryMethod = File.ReadAllText("../Matger.Repository/Data/DataSeed/DeliveryMethods.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethod);

                if (deliveryMethods?.Count > 0)
                {
                    foreach (var item in deliveryMethods)

                    {
                        await storDbcontrext.Set<DeliveryMethod>().AddAsync(item);
                    }
                    await storDbcontrext.SaveChangesAsync();

                }
                #endregion
            }



        }
    }
}
