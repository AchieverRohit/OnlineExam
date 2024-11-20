using EmailAuth.Common.Constants;
using EmailAuth.Common.Models;
using EmailAuth.Infrastructure.Clients.Interfaces;
using EmailAuth.Infrastructure.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailAuth.Infrastructure.Services
{
    /// <summary>
    /// Service for sending emails using SendGrid.
    /// </summary>
    internal class SendGridEmailService : IMailerService
    {
        private readonly SendGridDetail _sendGrid;
        private readonly ISendGridClient _sendGridClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridEmailService"/> class.
        /// <param name="sendGridClientFactory">The client factory of SendGrid.</param>
        /// </summary>
        /// <param name="sendGrid">The SendGrid details.</param>
        public SendGridEmailService(SendGridDetail sendGrid, ISendGridClientFactory sendGridClientFactory)
        {
            _sendGrid = sendGrid;
            _sendGridClient = sendGridClientFactory.Create();
        }

        /// <summary>
        /// Sends an email using a template.
        /// </summary>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="recipient">The recipient's email address.</param>
        /// <param name="keywordData">Dictionary of keywords to replace in the email body.</param>
        public async Task SendByTemplate(string subject, string body, string recipient, Dictionary<string, string> keywordData)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentNullException(nameof(subject));

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException(nameof(body));

            if (string.IsNullOrWhiteSpace(recipient))
                throw new ArgumentNullException(nameof(recipient));

            // Create email addresses for sender and recipient
            var from = new EmailAddress(_sendGrid.SenderEmail, _sendGrid.SenderDisplayName);
            var to = new EmailAddress(recipient);

            // Replace keywords in email body
            string emailBody = body;
            foreach (var keyword in keywordData)
            {
                emailBody = emailBody.Replace($"*{keyword.Key}*", keyword.Value);
            }

            // Create email message
            var emailMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);

            // Send email
            var response = await _sendGridClient.SendEmailAsync(emailMessage);

            // Check response status
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"{Constants.SendEmailFailed}: {response.StatusCode}");
            }
        }
    }
}
