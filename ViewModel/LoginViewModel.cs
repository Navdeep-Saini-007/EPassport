using System.ComponentModel.DataAnnotations;

namespace EPassport.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string? UserType { get; set; }
        [Required]
        [EmailAddress]
        public string? LoginId { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
