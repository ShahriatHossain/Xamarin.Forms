using Microsoft.EntityFrameworkCore;
using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class InstituteRepository : BaseRepository<Institute>, IInstituteRepository
    {
        public InstituteRepository(PigeonDbContext context) : base(context)
        {

        }

        public override Institute GetById(int id)
        {
            return SafeCall(() => this.Context.Institutes
            .Include(x => x.DomainInstitutes)
            .Include(x => x.PricingType)
            .FirstOrDefault(x => x.Id == id));
        }

        public override IEnumerable<Institute> GetBy(Expression<Func<Institute, bool>> predicate)
        {
            return SafeCall(() => GetBy1(predicate));
        }

        private IQueryable<Institute> GetBy1(Expression<Func<Institute, bool>> predicate)
        {
            var institutes =
             this.Context.Institutes.Where(predicate).Include(u => u.Creator).Include(x => x.InstituteSubscribes);
            foreach (var item in institutes)
            {
                var channels = this.Context.Channels.Where(m => m.InstituteId == item.Id).Count();
            }

            return institutes;
        }
    }
}