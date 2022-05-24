﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPassport.Models
{
    public partial class LoginCredential
    {
        [Key]
        public int Id { get; set; }
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

        public virtual RegistrationDetail? Login { get; set; }
    }
}
