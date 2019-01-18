using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pigeon.DataAccess.Repository
{
    public interface IInstituteRepository : IRepository<Institute>
    {
        IEnumerable<Institute> GetBy(Expression<Func<Institute, bool>> predicate);
    }
}
