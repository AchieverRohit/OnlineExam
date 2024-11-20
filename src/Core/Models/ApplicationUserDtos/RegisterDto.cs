using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Core.Models.ApplicationUserDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Firstname Required")]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Lastname Required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please Enter the password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least one lowercase letter, " +
            "one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter the Email")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the mobile number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit number")]
        public string MobileNo { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
