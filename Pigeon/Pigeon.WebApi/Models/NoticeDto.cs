using Newtonsoft.Json;
using Pigeon.DataAccess.Entities;
using System;

namespace Pigeon.WebApi.Models
{
    public class NoticeDto : BaseDto<Notice>
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; private set; }

        [JsonProperty("channel_id")]
        public int ChannelId { get; private set; }

        [JsonProperty("is_voting_enabled")]
        public bool IsVotingEnabled { get; private set; }

        [JsonProperty("voting_last_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? VotingLastDate { get; private set; }

        [JsonProperty("creation_time")]
        public DateTime? CreationTime { get; private set; }

        [JsonProperty("media_base_url", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaBaseUrl { get; private set; }

        [JsonProperty("display_media_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayMediaName { get; private set; }

        [JsonProperty("media_name", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaName { get; private set; }

        [JsonProperty("media_Type", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaType { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty(PropertyName = "voting_status")]
        public string VotingStatus { get; set; }

        public override void Map(Notice model)
        {
            Id = model.Id;
            ChannelId = model.ChannelId;
            VotingLastDate = model.VotingLastDate;
            CreationTime = model.CreationTime;
            IsVotingEnabled = model.IsVotingEnabled;
            Type = model.NoticeType == 1 ? "Text" : "Media";
            VotingStatus = string.Empty;

            if (model is TextNotice textNotice)
            {
                Text = textNotice.Text;
            }
            else if (model is MediaNotice mediaNotice)
            {
                MediaBaseUrl = "https://implevistabd.blob.core.windows.net/pigeoncontainer";
                DisplayMediaName = mediaNotice.FileDisplayName;
                MediaName = mediaNotice.FileName;
                MediaType = mediaNotice.FileType;
            }
        }
    }
}
