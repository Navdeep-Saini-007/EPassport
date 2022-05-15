using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class SupportingDocumentDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public byte[]? Document1 { get; set; }
        [Required]
        public byte[]? Document2 { get; set; }
        public int? ApplicantId { get; set; }

        public virtual ApplicationDetail? Applicant { get; set; }
    }
}
