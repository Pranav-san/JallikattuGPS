using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jallikattu.Data.ViewModels
{
    public class RegisterVM
    {

        [Required(ErrorMessage ="User Name Required")]
        [StringLength(20, MinimumLength = 3,ErrorMessage = "Full name must be between 3 and 20 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="EmailAddress Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required (ErrorMessage ="Password Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }


    }
}