using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        // DataAnnotations are useful for validation in ASP.net core/framework
        // use this from UI to service layer.
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage ="Make sure password length is between 8 and 20.", MinimumLength =8)]
        //password should be strong, one Upper, Lower, number, sepcial character. 
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
