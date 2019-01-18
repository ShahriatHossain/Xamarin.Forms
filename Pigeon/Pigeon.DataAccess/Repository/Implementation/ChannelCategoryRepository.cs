using Pigeon.DataAccess.Entities;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class ChannelCategoryRepository : BaseRepository<ChannelCategory>, IChannelCategoryRepository
    {
        public ChannelCategoryRepository(PigeonDbContext context) : base(context)
        {

        }
    }

}
