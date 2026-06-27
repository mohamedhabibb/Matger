using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matger.Core.Entities;
using Matger.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Matger.Repository
{
    public class SpecificationsEvalutor<T> where T : BaseEntity
    {

        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecifications<T> Spec)
        {
            var Query = InputQuery;

            if (Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }

            if (Spec.OrderBy is not null)
            {
                Query = Query.OrderBy(Spec.OrderBy);
            }

            if (Spec.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDescending);
            }

            if (Spec.IsPaginationEnabled)
            {
                Query = Query.Skip(Spec.skip).Take(Spec.take);
            }

            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return Query;

        }
    }
}
