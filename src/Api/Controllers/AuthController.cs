using Microsoft.AspNetCore.Identity;
using thinkschool.OnlineExam.Core.Models.ApplicationUserDtos;

namespace thinkschool.OnlineExam.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly AppDbContext _context;
        private readonly IApplicationUserService _applicationUserService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context,
            IApplicationUserService applicationUserService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _applicationUserService = applicationUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> ResiterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var applicationUser = new ApplicationUser
                {
                    FirstName = registerDto.Firstname,
                    LastName = registerDto.Lastname,
                    PhoneNumber = registerDto.MobileNo,
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                };

                // Create the user and hash the password
                var createdUser = await _userManager.CreateAsync(applicationUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    // Assign the role
                    var roleResult = await _userManager.AddToRoleAsync(applicationUser, registerDto.Role);
                    if (roleResult.Succeeded)
                    {
                        

                        // Get the assigned role
                        var roles = await _userManager.GetRolesAsync(applicationUser);

                        return Ok(new NewUserDto
                        {
                            UserId = applicationUser.Id,
                            FirstName = applicationUser.FirstName,
                            LastName = applicationUser.LastName,
                            Email = applicationUser.Email,
                            MobileNo = applicationUser.PhoneNumber,
                            Role = roles.FirstOrDefault()
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find the user by email
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null)
                return Unauthorized("Invalid Email!");

            // Verify password
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Username not found and/or password incorrect");

            // Get the user roles
            var roles = await _userManager.GetRolesAsync(user);

            // Create the logged-in user response
            var loggedInUser = new LoggedInUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                MobileNo = user.PhoneNumber,
                Role = roles.FirstOrDefault(),
                UserId = user.Id,
                Token = await _tokenService.CreateToken(user),
                Expiration = DateTime.Now.AddDays(3),
            };

            return Ok(loggedInUser);
        }

        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(user != null); // True if email exists, otherwise false
        }

        /// <summary>
        /// Handles HTTP GET requests to retrieve types.
        /// </summary>
        /// <param name="type">The type of data to retrieve, included in the request body.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>An ActionResult containing a ListResponse of strings.</returns>
        [HttpPost("GetTypes")]
        public async Task<ActionResult<ListResponse<string>>> GetTypes([FromBody] string type, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return BadRequest("Type parameter cannot be empty.");
            }

            var result = await _applicationUserService.GetTypes(type, cancellationToken);
            return HandleResult(result);
        }
    }
}
