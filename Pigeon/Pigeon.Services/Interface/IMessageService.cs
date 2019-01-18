using Pigeon.Services.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Interface
{
    public interface IMessageService
    {
        Task<ChannelNotice> Save(TextMessage message, ChannelApi channel);
        Task<ObservableCollection<ChannelNotice>> Get(int channelId, int? sequenceId);
        Task<bool> SaveVote(int messageId, string voteType);
        Task<NoticeDetail> GetDetail(int noticeId);
        Task<ChannelNotice> SaveMediaNoticeAsync(int? channelId, bool isVotingEnabled, DateTime? votingLastDate, dynamic file, string fileType);
        Task<IEnumerable<ChannelNotice>> GetNoticeDetail(int noticeId);
    }
}
