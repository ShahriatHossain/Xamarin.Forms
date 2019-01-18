using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("PigeonUser")]
    public class PigeonUser : IdentityUser
    {
        [Key]
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [MaxLength(15)]
        public string CountryCode { get; set; }

        [Required]
        [MaxLength(15)]
        public string MobileNo { get; set; }

        public int PricingTypeId { get; set; }

        //public PricingType PricingType { get; set; }

        //public int? InstituteId { get; set; }

        //public Institute Institute { get; set; }

        public List<UserInstitute> UserInstitutes { get; set; }
    }
}
