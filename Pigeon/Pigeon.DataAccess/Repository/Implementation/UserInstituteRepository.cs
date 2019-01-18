using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class UserInstituteRepository : BaseRepository<UserInstitute>, IUserInstituteRepository
    {
        public UserInstituteRepository(PigeonDbContext context):base(context)
        {

        }

        public bool Save(List<UserInstitute> userInstitutes)
        {
            return SafeCall(() =>
            {
                userInstitutes.ForEach(x => this.Context.Add(x));                
                this.Context.SaveChanges();
                return true;
            });
        }

        public override IEnumerable<UserInstitute> GetBy(Expression<Func<UserInstitute, bool>> predicate)
        {            
            return SafeCall(() => this.Context.UserInstitutes.Include("Institute").Where(predicate));
        }

    }
}
