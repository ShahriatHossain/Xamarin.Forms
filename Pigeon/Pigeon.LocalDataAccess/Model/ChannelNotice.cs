using System;
namespace Pigeon.LocalDataAccess.Model
{
    public class ChannelNotice : BaseItem
    {
        public int NoticeId { get; set; }

        public string UserId { get; set; }

        public int ChannelId { get; set; }

        public string Notice { get; set; }
        public bool ResponseNeeded { get; set; }
        public string FileType { get; set; }
        public string FileDisplayName { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? VotingLastDate { get; set; }
        public bool IsVoteCasted { get; set; }

        public string VotingStatus { get; set; }
        public string MediaBasedUrl { get; set; }

        public string MediaName { get; set; }
    }
}

