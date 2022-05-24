using System.ComponentModel.DataAnnotations;
using EPassport.Models;

namespace EPassport.ViewModel
{
    public class DocumentCreateViewModel
    {
        [Required]
        public IFormFile Document1 { get; set; }
        [Required]
        public IFormFile Document2 { get; set; }
        public int? ApplicantId { get; set; }

        public virtual ApplicationDetail? Applicant { get; set; }
    }
}
