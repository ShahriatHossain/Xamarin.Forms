using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("Channel")]
    public class Channel : BaseModel
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public string Status { get; set; }

        [MaxLength(4)]
        public string SecurePin { get; set; }

        public DateTime RecordUpdatedOn { get; set; }

        public bool OwnerFlag { get; set; }

        public int MaxSeqID { get; set; }

        public bool IsVerified { get; set; }

        public bool IsExpired { get; set; }

        public string NewNoticeIndicator { get; set; }

        public int InstituteId { get; set; }

        [ForeignKey("InstituteId")]
        public Institute Institute { get; set; }

        public string CreatorId { get; set; }
        public PigeonUser Creator { get; set; }

        public string Logo { get; set; }

        public bool IsDefault { get; set; }


        public int ChannelCategoryId { get; set; }

        [ForeignKey("ChannelCategoryId")]
        public ChannelCategory ChannelCategory { get; set; }

        public List<ChannelSubscribe> ChannelSubscribes { get; set; }
    }
}
