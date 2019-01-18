using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Models
{
    public class UserProfileModel
    {
        public string CountryCode { get; set; }

        public string MobileNo { get; set; }
    }
    public class RegistrationModel : UserProfileModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }
}
