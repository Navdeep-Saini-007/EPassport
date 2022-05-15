using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class RegistrationDetail
    {
        public RegistrationDetail()
        {
            ApplicationDetails = new HashSet<ApplicationDetail>();
        }

        [Key]
        public string LoginId { get; set; } = null!;
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime? Dob { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? EmailId { get; set; }
        [Required]
        public string? PhoneNo { get; set; }

        public virtual ICollection<ApplicationDetail> ApplicationDetails { get; set; }
    }
}
