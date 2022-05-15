using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class AddressDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? HouseNo { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? District { get; set; }
        [Required]
        public int? Pincode { get; set; }
        [Required]
        public string? TelephoneNumber { get; set; }
        [Required]
        public string? EmailId { get; set; }
        public int? ApplicantId { get; set; }

        public virtual ApplicationDetail? Applicant { get; set; }
    }
}
