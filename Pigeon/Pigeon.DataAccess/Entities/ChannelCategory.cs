using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("ChannelCategory")]
    public class ChannelCategory : BaseModel
    {
        public string Name { get; set; }
    }
}
