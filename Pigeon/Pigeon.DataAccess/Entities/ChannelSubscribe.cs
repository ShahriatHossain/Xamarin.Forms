using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("ChannelSubscribe")]
    public class ChannelSubscribe : BaseModel
    {
        public int ChannelId { get; set; }

        public string UserId { get; set; }
    }
}
