namespace EmailAuth.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// Interface for mailer service.
    /// </summary>
    internal interface IMailerService
    {
        /// <summary>
        /// Sends an email using a template.
        /// </summary>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="recipient">The recipient's email address.</param>
        /// <param name="keywordData">Dictionary of keywords to replace in the email body.</param>
        Task SendByTemplate(string subject, string body, string recipient, Dictionary<string, string> keywordData);
    }
}
