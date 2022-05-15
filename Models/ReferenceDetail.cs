using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class ReferenceDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ReferenceName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? TelephoneNumber { get; set; }
        public int? ApplicantId { get; set; }

        public virtual ApplicationDetail? Applicant { get; set; }
    }
}
