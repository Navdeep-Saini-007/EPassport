using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class PassportOffice
    {
        [Key]
        public int OfficeId { get; set; }
        [Required]
        public string? OfficeName { get; set; }
        [Required]
        public string? Jurisdiction { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public int? ApplicantId { get; set; }

        public virtual ApplicationDetail? Applicant { get; set; }
    }
}
