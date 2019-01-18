using Newtonsoft.Json;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class MessageService : BaseService, IMessageService
    {
        public async Task<ObservableCollection<ChannelNotice>> Get(int channelId, int? sequenceId)
        {
            var action = "notice?channelId=" + channelId + "&lastReceivednoticeId=" + sequenceId;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<ChannelNotice>>(response);
        }

        public async Task<IEnumerable<ChannelNotice>> GetNoticeDetail(int noticeId)
        {
            var action = "notice?noticeId=" + noticeId;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<IEnumerable<ChannelNotice>>(response);
        }

        public async Task<ChannelNotice> Save(TextMessage message, ChannelApi channel)
        {
            var votingLastDate = (message.VotingExpireDate == null) ? null
                : ((DateTime)message.VotingExpireDate).ToString("dd-MM-yyyy hh:mm:ss.ffffffzzz");

            var response = await PostAndReadAsStringAsync("notice", true
                , CreateInput("Text", message.Text)
                , CreateInput("ChannelId", channel.Id)
                //, CreateInput("secure_pin", channel.SecurePin)
                , CreateInput("IsVotingEnabled", message.EnableVoting ? true : false)
                , CreateInput("VotingLastDate", votingLastDate)
                //, CreateInput("file_type", message.FileType)
                //, CreateInput("file_display_name", message.FileDisplayName)
                );

            var jObject = JsonConvert.DeserializeObject<ChannelNotice>(response);
            return jObject;
        }

        public async Task<bool> SaveVote(int messageId, string voteType)
        {
            var action = "notice/" + messageId + "/voting/" + voteType;
            var response = await PostAndReadAsStringAsync(action, true);

            return true;
        }

        public async Task<NoticeDetail> GetDetail(int noticeId)
        {
            var action = "notice/" + noticeId + "/voting/result";
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<NoticeDetail>(response);
        }

        public async Task<ChannelNotice> SaveMediaNoticeAsync(int? channelId, bool isVotingEnabled, DateTime? votingLastDate, dynamic file, string fileType)
        {
            var votingLastDate2 = (votingLastDate == null) ? null
                : ((DateTime)votingLastDate).ToString("dd-MM-yyyy hh:mm:ss");

            var response = await UploadAndReadAsStringAsync("notice/media/channel/" + channelId + "/" + isVotingEnabled + "?votingLastDate=" + votingLastDate2, true, file, fileType);
            var jObject = JsonConvert.DeserializeObject<ChannelNotice>(response);
            return jObject;
        }
    }
}
