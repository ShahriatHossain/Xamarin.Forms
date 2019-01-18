using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("Institute")]
    public class Institute : BaseModel
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public string Area { get; set; }

        public string District { get; set; }

        public string Division { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        public string CreatorId { get; set; }

        public PigeonUser Creator { get; set; }

        public DateTime CreationTime { get; set; }

        public List<DomainInstitute> DomainInstitutes { get; set; }

        public int PricingTypeId { get; private set; } = 1;

        public PricingType PricingType { get; set; }

        public int InstituteCategoryId { get; set; }

        public InstituteCategory InstituteCategory { get; set; }

        public int InstituteTypeId { get; set; }

        public InstituteType InstituteType { get; set; }

        public List<InstituteSubscribe> InstituteSubscribes { get; set; }

    }
}
