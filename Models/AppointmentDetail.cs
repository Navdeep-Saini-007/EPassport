using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class AppointmentDetail
    {
        [Key]
        public int MonthId { get; set; }
        [Required]
        public string? MonthName { get; set; }
        [Required]
        public string? AvailableDays { get; set; }
        [Required]
        public string? TimeSlots { get; set; }
        public int? ApplicantId { get; set; }

        public virtual ApplicationDetail? Applicant { get; set; }
    }
}
