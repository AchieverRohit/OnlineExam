namespace EmailAuth.Common.Constants
{
    internal static class EmailConstants
    {
        public const string LoginEmailTitle = "Login request for your account";
        public const string LoginEmailBody = @"
        <html>
        <body style=""text-align: center; align-items: center; justify-items: center; justify-content: center; font-family: 'Avenir', sans-serif;color: #555555;"">

        <p style=""font-size:40px;margin: 0 auto;"">Login Request</p>

        <div style=""background:#555555; height:4px; width:150px; margin: 10px auto;""></div>

        <div>
        <br/><br/>
        We received a login request from your email. Here is the OTP to setup password for your account.
        <br/><br/>

        <div style=""display: inline-block; border: 1px solid transparent; border-radius: 10px; padding: 0px 50px;background-color: #F2994A4D;"">
            <p style=""font-weight: 500; font-size: x-large;"">*OTP*</p>                                
        </div>

        <br/><br/>
        See you onboarded!
        <br/>
        Thanks

        </div>

        </body>
        </html>
        ";
        public const string OTP = "OTP";
        public const string ResetPasswordEmailTitle = "Reset password request for you account";
        public const string ResetPasswordEmailBody = @"
        <html>
        <body style=""text-align: center; align-items: center; justify-items: center; justify-content: center; font-family: 'Avenir', sans-serif;color: #555555;"">

        <p style=""font-size:40px;margin: 0 auto;"">Reset Password Request</p>

        <div style=""background:#555555; height:4px; width:150px; margin: 10px auto;""></div>

        <div>
        <br/><br/>
        We received a password reset request from your email. Here is the OTP to setup new password for your account.
        <br/><br/>

        <div style=""display: inline-block; border: 1px solid transparent; border-radius: 10px; padding: 0px 50px;background-color: #F2994A4D;"">
            <p style=""font-weight: 500; font-size: x-large;"">*OTP*</p>                                
        </div>

        <br/><br/>
        Thanks

        </div>

        </body>
        </html>

        ";
    }
}
