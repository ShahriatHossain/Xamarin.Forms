using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pigeon.DataAccess.Entities
{
    [Table("NoticeVoting")]
    public class NoticeVoting : BaseModel
    {
        public int ChannelId { get; set; }

        public int NoticeId { get; set; }

        public string UserId { get; set; }

        public VotingType ResponseType { get; set; }
    }

    public enum VotingType
    {
        Up = 1,
        Down = 2
    }
}
