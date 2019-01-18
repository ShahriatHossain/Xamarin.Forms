using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pigeon.DataAccess.Entities
{
    public class DomainInstitute : BaseModel
    {
        [MaxLength(50)]        
        public string DomainName { get; set; }

        public int InstituteId { get; set; }
    }
}
