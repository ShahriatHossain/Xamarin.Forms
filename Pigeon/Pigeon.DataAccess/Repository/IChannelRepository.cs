using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Pigeon.DataAccess.Repository
{
    public interface IChannelRepository : IRepository<Channel>
    {
        IEnumerable<Channel> GetBy(Expression<Func<Channel, bool>> predicate);
        void SetSecurePin(int channelId, string securePin);

        int Count(string userId);
    }
}
