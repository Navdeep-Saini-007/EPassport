using System;
using System.Collections.Generic;

namespace EPassport.Models
{
    public partial class LoginCredential
    {
        public string? UserType { get; set; }
        public string? LoginId { get; set; }
        public string? Password { get; set; }

        public virtual RegistrationDetail? Login { get; set; }
    }
}
