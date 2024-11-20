namespace EmailAuth.Common.Constants
{
    /// <summary>
    /// Class to hold constant values used across the application.
    /// </summary>
    internal static class Constants
    {
        public const string JWT_ValidIssuer = "JWT:ValidIssuer";
        public const string JWT_ValidAudience = "JWT:ValidAudience";
        public const string JWT_Secret = "JWT:Secret";
        public const string Email_SendGridApiKey = "Email:SendGridApiKey";
        public const string Email_EmailFromAddress = "Email:EmailFromAddress";
        public const string Email_EmailFromName = "Email:EmailFromName";
        public const string IsTokenExpired = "IS-TOKEN-EXPIRED";
        public const string True = "true";
        public const string Authorization = "Authorization";
        public const string BearerFormat = "JWT";
        public const string SwaggerAuthDescription = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.

                        Example: ""Bearer 12345abcdef""";
        public const string InvalidEmail = "Invalid Email.";
        public const string MapFrom = "MapFrom";
        public const string MapTo = "MapTo";
        public const string InvalidToken = "Invalid token.";
        public const string UserExist = "User already exists.";
        public const string UserNotFound = "User not found.";
        public const string UserProfileSaved = "User Profile saved successfully.";
        public const string VerifyEmail = "Please verify your email first.";
        public const string SetupAccount = "Please setup your account first.";
        public const string InvalidEmailOrPassword = "Invalid Email & Password combination.";
        public const string OTPSent = "OTP has been sent to your email.";
        public const string ValidateOTP = "Please validate your OTP first.";
        public const string InvalidOTP = "Invalid OTP.";
        public const string ValidOTP = "Valid OTP.";
        public const string PasswordSent = "Password has been set successfully.";
        public const string InvalidClientReq = "Invalid client request.";
        public const string InternalServiceError = "An error occurred while processing the request.";
        public const string SendEmailFailed = "Failed to send email";
        public const string InvalidEmailVal = "Please provide a valid email address.";
        public const string ReqAccessTokenVal = "Access token is required.";
        public const string ReqRefreshTokenVal = "Refresh token is required.";
        public const string ReqOTPVal = "OTP is required.";
        public const string ReqPasswordVal = "Password is required.";
        public const string PasswordLengthVal = "Please provide a password of at least 8 characters long.";
        public const string PasswordLongVal = "Too long password. Please reduce the password length.";
        public const string PasswordRegex = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$";
        public const string InvalidPasswordVal = "Invalid Password. At least one small and one capital alphabet, one number, and one special character is required.";
        public const string SaltSizeError = "Salt size must be {0} bytes.";
    }
}
