using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class InstituteTypeRepository : BaseRepository<InstituteType>, IInstituteTypeRepository
    {
        public InstituteTypeRepository(PigeonDbContext context):base(context)
        {

        }
    
    }
}
