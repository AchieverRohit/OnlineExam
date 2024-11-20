using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Services.IServices
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);

    }
}
