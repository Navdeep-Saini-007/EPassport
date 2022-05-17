using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class ApplicationDetail
    {
        public ApplicationDetail()
        {
            AddressDetails = new HashSet<AddressDetail>();
            AppointmentDetails = new HashSet<AppointmentDetail>();
            FamilyDetails = new HashSet<FamilyDetail>();
            PassportOffices = new HashSet<PassportOffice>();
            ReferenceDetails = new HashSet<ReferenceDetail>();
            SupportingDocumentDetails = new HashSet<SupportingDocumentDetail>();
        }

        [Key]
        public int ApplicantId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime? Dob { get; set; }
        [Required]
        public string? PlaceOfBirth { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? District { get; set; }
        [Required]
        public string? MaritalStatus { get; set; }
        [Required]
        public string? Pan { get; set; }
        [Required]
        public string? EmploymentType { get; set; }
        [Required]
        public string? EducationalQualification { get; set; }
        public string? PassportStatus { get; set; }
        public string? LoginId { get; set; }

        public virtual RegistrationDetail? Login { get; set; }
        public virtual ICollection<AddressDetail> AddressDetails { get; set; }
        public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; }
        public virtual ICollection<FamilyDetail> FamilyDetails { get; set; }
        public virtual ICollection<PassportOffice> PassportOffices { get; set; }
        public virtual ICollection<ReferenceDetail> ReferenceDetails { get; set; }
        public virtual ICollection<SupportingDocumentDetail> SupportingDocumentDetails { get; set; }
    }
}
