using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace History_Web
{
    public class LoginModel
    {
        public string Email { get; set; }
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string message { get; set; }
        public string ReturnURL { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}