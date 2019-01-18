using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pigeon.DataAccess.Entities
{
    [Table("InstituteCategory")]
    public class InstituteCategory : BaseModel
    {
        public string Name { get; set; }
    }
}
