using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;
using Matger.Core.Repository;
using Matger.Core.Specifications;
using Matger.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Matger.Repository
{
    public class GenericRepository<T> :IGenericRepository<T> where T:BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {

            if (typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>)await _storeContext.Set<Product>().Include(p => p.ProductBrand).Include(p => p.ProductTypes).ToListAsync();
            }

            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        => await _storeContext.Set<T>().FindAsync(id);




        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }
        public async Task<T> GetEntityWithSpecAsync(ISpecifications<T> Spec) //GetEntityWithSpecAsync
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).CountAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecifications<T> Spec)
        {
            return SpecificationsEvalutor<T>.GetQuery(_storeContext.Set<T>(), Spec);
        }


        public async Task Add(T Item)
        {
            await _storeContext.Set<T>().AddAsync(Item);
        }

        public void Delete(T Item)
        {
            _storeContext.Set<T>().Remove(Item);
        }
    }
}
