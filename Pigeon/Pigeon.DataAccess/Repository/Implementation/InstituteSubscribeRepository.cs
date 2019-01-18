using Pigeon.DataAccess.Entities;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class InstituteSubscribeRepository : BaseRepository<InstituteSubscribe>, IInstituteSubscribeRepository
    {
        public InstituteSubscribeRepository(PigeonDbContext context) : base(context)
        {

        }
    }
}
