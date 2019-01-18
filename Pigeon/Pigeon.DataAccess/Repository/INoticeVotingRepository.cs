using Pigeon.DataAccess.Entities;

namespace Pigeon.DataAccess.Repository
{
    public interface INoticeVotingRepository : IRepository<NoticeVoting>
    {
        void GetResult(int noticeId, out int upVoteCount, out int downVoteCoun);
    }
}
