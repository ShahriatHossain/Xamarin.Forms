using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pigeon.DataAccess.Entities
{
    [Table("InstituteType")]
    public class InstituteType : BaseModel
    {
        public string Type { get; set; }
    }
}
