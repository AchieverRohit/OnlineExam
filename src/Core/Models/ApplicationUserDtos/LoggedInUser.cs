﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Core.Models.ApplicationUserDtos
{
    public class LoggedInUser
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }
        public string? MobileNo { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
        public string ExamUserId { get; set; }

        public DateTime Expiration { get; set; }
    }
}