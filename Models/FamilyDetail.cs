using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class FamilyDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? FathersName { get; set; }
        [Required]
        public string? MothersName { get; set; }
        public int? ApplicantId { get; set; }

        public virtual ApplicationDetail? Applicant { get; set; }
    }
}
