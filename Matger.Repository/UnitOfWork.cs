using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;
using Matger.Core.Repository;
using Matger.Core;
using Matger.Repository.Data;

namespace Matger.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositories = new Hashtable();  //to store all repo
        }
        public async Task<int> CompleteAsync()
        => await _storeContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
       => _storeContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;  // product , order >> as string only
            if (!_repositories.ContainsKey(type)) //To crerate and add in hashtable 
            {
                var Repository = new GenericRepository<TEntity>(_storeContext);//create
                _repositories.Add(type, Repository);
            }
            return _repositories[type] as GenericRepository<TEntity>;
        }
    }
}
