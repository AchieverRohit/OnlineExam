using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Core.Models.ApplicationUserDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Please enter the Email")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter the password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least one lowercase letter, " +
            "one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }
    }
}
