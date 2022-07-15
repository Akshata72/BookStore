using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class UserReg
    {
        [Required]
        [RegularExpression(@"(?=^.{0,40}$)^[a-zA-Z]{3,}\s?[a-zA-Z]*$", ErrorMessage = "Full name is not valid")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression("^[a-z 0-9]{3,}[@][a-z]{4,}[.][a-z]{3,}$", ErrorMessage = "Please Enter a Valid Email")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "Please Enter a Valid Password")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]{9}$", ErrorMessage = "Mobile Number is not valid")]
        public long MobileNumber { get; set; }
    }
}
