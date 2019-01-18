using Pigeon.DataAccess.Entities;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class ChannelSubscribeRepository : BaseRepository<ChannelSubscribe>, IChannelSubscribeRepository
    {
        public ChannelSubscribeRepository(PigeonDbContext context) : base(context)
        {

        }
    }
}