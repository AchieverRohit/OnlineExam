


using Microsoft.AspNetCore.Identity;

namespace thinkschool.OnlineExam.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public virtual List<UserExam> UserExamUsers { get; set; }
    }
}

