using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("InstituteSubscribe")]
    public class InstituteSubscribe : BaseModel
    {
        public int InstituteId { get; set; }

        public string UserId { get; set; }
    }
}
