using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Pigeon.Services.Model
{
    public class ChannelNotice
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "channel_id")]

        public int ChannelId { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Notice { get; set; }
        [JsonProperty(PropertyName = "is_voting_enabled")]
        public bool ResponseNeeded { get; set; }

        [JsonProperty(PropertyName = "file_type")]
        public string FileType { get; set; }

        [JsonProperty(PropertyName = "file_display_name")]
        public string FileDisplayName { get; set; }

        [JsonProperty(PropertyName = "creation_time")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreationTime { get; set; }

        [JsonProperty(PropertyName = "voting_last_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? VotingLastDate { get; set; }

        [JsonProperty(PropertyName = "voting_status")]
        public string VotingStatus { get; set; }

        [JsonProperty(PropertyName = "media_base_url")]
        public string MediaBasedUrl { get; set; }

        [JsonProperty(PropertyName = "display_media_name")]
        public string MediaDisplayName { get; set; }

        [JsonProperty(PropertyName = "media_name")]
        public string MediaName { get; set; }

        [JsonProperty(PropertyName = "media_Type")]
        public string MediaType { get; set; }

    }
}
