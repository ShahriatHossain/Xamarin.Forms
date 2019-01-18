using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class NoticeVotingRepository : BaseRepository<NoticeVoting>, INoticeVotingRepository
    {
        public NoticeVotingRepository(PigeonDbContext context):base(context)
        {

        }

        public void GetResult(int noticeId, out int upVoteCount, out int downVoteCount)
        {
            var responses = this.Context.NoticeVotings.Where(x => x.NoticeId == noticeId).Select(x => x.ResponseType).ToList();
            upVoteCount = responses.Count(x => x == VotingType.Up);
            downVoteCount = responses.Count(x => x == VotingType.Down);
        }
    }
}
