using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;

namespace Matger.Core.Specifications
{
    public interface ISpecifications< T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression <Func<T, object>> OrderBy { get; set; }

        public Expression< Func<T, object>> OrderByDescending { get; set; }

        public int skip { get; set; }
        public int take { get; set; }

        public bool IsPaginationEnabled { get; set; }



    }
}
