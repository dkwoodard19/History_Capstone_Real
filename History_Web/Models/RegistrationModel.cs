using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace History_Web
{
    public class RegistrationModel
    {
        public string Message { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(Constants.MaxUserNameLength, ErrorMessage = "The UserName must be between {2} and {1} characters long.", MinimumLength = Constants.MinUserNameLength)]
        //[DataType(DataType.Text)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [StringLength(Constants.MaxPasswordLength, ErrorMessage = "The Password must be between {2} and {1} characters long.", MinimumLength = Constants.MinPasswordLength)]
        [RegularExpression(Constants.PasswordRequirements, ErrorMessage = Constants.PasswordRequirementsMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Again")]
        public string PasswordAgain { get; set; }
        public string ReturnURL { get; set; }
    }
}