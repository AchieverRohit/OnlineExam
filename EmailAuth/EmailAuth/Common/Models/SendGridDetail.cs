namespace EmailAuth.Common.Models
{
    /// <summary>
    /// Model representing SendGrid configuration details.
    /// </summary>
    public class SendGridDetail
    {
        /// <summary>
        /// Gets or sets the API key for SendGrid.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the sender email address.
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the display name for the sender.
        /// </summary>
        public string SenderDisplayName { get; set; }
    }
}
