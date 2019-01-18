using Pigeon.DataAccess.Entities;
using System;

namespace Pigeon.DataAccess.Repository
{
    public interface INoticeRepository : IRepository<Notice>
    {
        int Count(int channelId);
        bool IsValidByRefreshDate(int channelId, DateTime lastRefreshDate);
    }
}
