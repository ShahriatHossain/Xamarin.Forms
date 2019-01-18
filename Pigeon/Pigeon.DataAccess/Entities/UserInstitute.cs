using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("UserInstitute")]
    public class UserInstitute : BaseModel
    {
        public string UserId { get; set; }

        public int InstituteId { get; set; }

        public Institute Institute { get; set; }
    }
}
