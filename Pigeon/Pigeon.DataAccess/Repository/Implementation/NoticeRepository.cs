using Pigeon.DataAccess.Entities;
using System;
using System.Linq;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class NoticeRepository : BaseRepository<Notice>, INoticeRepository
    {
        public NoticeRepository(PigeonDbContext context) : base(context)
        {

        }

        public int Count(int channelId)
        {
            return SafeCall(() => this.Context.Notices.Count(x => x.ChannelId == channelId));
        }

        public bool IsValidByRefreshDate(int channelId, DateTime lastRefreshDate)
        {
            return SafeCall(() => this.Context.Notices
                    .Where(x => x.ChannelId == channelId))
                    .ToList()
                    .Any(x => x.CreationTime > lastRefreshDate);

        }
    }
}
