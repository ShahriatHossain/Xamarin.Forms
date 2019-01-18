using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class ChannelRepository: BaseRepository<Channel>, IChannelRepository         
    {
        public ChannelRepository(PigeonDbContext context):base(context)
        {
            
        }


        public void SetSecurePin(int channelId, string securePin)
        {
            var channel = this.GetById(channelId);
            channel.SecurePin = securePin;
            Save(channel);
        }

        public int Count(string userId)
        {
            return SafeCall(() => this.Context.Channels.Count(x => x.CreatorId == userId));
        }

        public override IEnumerable<Channel> GetBy(Expression<Func<Channel, bool>> predicate)
        {
            return SafeCall(() => this.Context.Channels.Where(predicate).Include(u => u.Creator).Include(i => i.Institute).Include(x => x.ChannelSubscribes));            
        }
    }
}
