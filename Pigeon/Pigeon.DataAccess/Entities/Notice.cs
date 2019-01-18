using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("Notice")]
    public abstract class Notice : BaseModel
    {
        public int ChannelId { get; set; }

        public Channel Channel { get; set; }

        public bool IsVotingEnabled { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? VotingLastDate { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

        public int NoticeType { get; set; }
    }
    public class TextNotice : Notice
    {
        public TextNotice()
        {
            this.NoticeType = 1;
        }
        public string Text { get; set; }
    }
    public class MediaNotice : Notice
    {
        public MediaNotice()
        {
            this.NoticeType = 2;
        }
        public string FileType { get; set; }
        public string FileDisplayName { get; set; }
        public string FileName { get; set; }
    }
}
