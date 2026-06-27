using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;
using Matger.Core.Specifications;

namespace Matger.Core.Repository
{
    public interface IGenericRepository<T>  where T:BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);


        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);

        Task<T> GetEntityWithSpecAsync(ISpecifications<T> Spec);

        Task<int> CountWithSpecAsync(ISpecifications<T> Spec);

        Task Add(T Item);

        void Delete(T Item);
    }
}
