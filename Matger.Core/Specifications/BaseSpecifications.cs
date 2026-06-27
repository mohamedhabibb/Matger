using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;

namespace Matger.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression <Func<T, object>> OrderBy { get ; set ; }

        public Expression<Func<T, object>> OrderByDescending { get ; set; }

        public int skip { get; set; }
        public int take { get; set; }

        public bool IsPaginationEnabled { get; set; }


        public BaseSpecifications()
        {

        }
        public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDescending = OrderByDescExpression;
        }

        public void ApplyPagination(int Skip, int Take)
        {
            skip = Skip;
            take = Take;
            IsPaginationEnabled = true;
        }

    }
}
