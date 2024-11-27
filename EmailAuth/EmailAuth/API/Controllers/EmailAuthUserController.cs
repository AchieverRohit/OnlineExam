using EmailAuth.Application.DTOs;
using EmailAuth.Application.Interfaces;
using EmailAuth.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmailAuth.API.Controllers
{
    /// <summary>
    /// Controller for handling email authentication related user actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class EmailAuthUserController : ControllerBase
    {
        private readonly IEmailAuthUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAuthUserController"/> class.
        /// </summary>
        /// <param name="userService">Service for email authentication user operations.</param>
        public EmailAuthUserController(IEmailAuthUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="dto">Data transfer object for adding a new user.</param>
        /// <returns>Action result of the add user operation.</returns>
        [HttpPost("Signup")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto dto)
        {
            var result = await _userService.AddUserAsync(dto);
            return HandleResult(result);
        }

        /// <summary>
        /// Initiates the password reset process.
        /// </summary>
        /// <param name="dto">Data transfer object for initiating password reset.</param>
        /// <returns>Action result of the initiate reset password operation.</returns>
        [HttpPost("InitiatePasswordSet")]
        public async Task<IActionResult> InitResetPassword([FromBody] InitResetPasswordDto dto)
        {
            var result = await _userService.InitResetPasswordAsync(dto);
            return HandleResult(result);
        }

        /// <summary>
        /// Sets a new password for the user.
        /// </summary>
        /// <param name="dto">Data transfer object for setting a new password.</param>
        /// <returns>Action result of the set password operation.</returns>
        [HttpPost("SetPassword")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto dto)
        {
            var result = await _userService.SetPasswordAsync(dto);
            return HandleResult(result);
        }

        /// <summary>
        /// Validates the provided OTP.
        /// </summary>
        /// <param name="dto">Data transfer object for validating OTP.</param>
        /// <returns>Action result of the validate OTP operation.</returns>
        [HttpPost("ValidateOTP")]
        public async Task<IActionResult> ValidateOTP([FromBody] ValidateOTPDto dto)
        {
            var result = await _userService.ValidateOTPAsync(dto);
            return HandleResult(result);
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="request">Login request data.</param>
        /// <returns>Action result of the login operation.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);
            return HandleResult(result);
        }

        /// <summary>
        /// Saves the user profile.
        /// </summary>
        /// <param name="saveUserProfileDto">Data transfer object for saving user profile.</param>
        /// <returns>Action result of the save user profile operation.</returns>
        //[Authorize]
        [HttpPost("SaveProfile")]
        public async Task<ActionResult<BaseVM>> SaveUserProfile([FromBody] SaveUserProfileDto saveUserProfileDto)
        {
            var result = await _userService.SaveUserProfileAsync(saveUserProfileDto);
            return HandleResult(result);
        }

        /// <summary>
        /// Handles the result of service operations and returns appropriate HTTP response.
        /// </summary>
        /// <param name="result">The result of the service operation.</param>
        /// <param name="isFlat">Indicates if the result should be flattened.</param>
        /// <returns>Action result with appropriate HTTP status code.</returns>
        protected ActionResult HandleResult(BaseVM result, bool isFlat = false)
        {

            return result.Status switch
            {
                HttpStatusCode.OK => isFlat ? Ok((result as dynamic).Data) : Ok(result),
                HttpStatusCode.NotFound => NotFound(result),
                HttpStatusCode.BadRequest => BadRequest(result),
                _ => StatusCode((int)result.Status, result)
            };
        }
    }
}
