using System.Net;
using EmailAuth.Common.Constants;
using EmailAuth.Common.Models;
using EmailAuth.Infrastructure.Clients.Interfaces;
using EmailAuth.Infrastructure.Services;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailAuthTests.UnitTests.Infrastructure.Services
{
    public class SendGridEmailServiceTests
    {
        private readonly Mock<ISendGridClient> _sendGridClientMock;
        private readonly Mock<ISendGridClientFactory> _sendGridClientFactoryMock;
        private readonly SendGridEmailService _sendGridEmailService;
        private readonly SendGridDetail _sendGridDetail;

        public SendGridEmailServiceTests()
        {
            _sendGridClientMock = new Mock<ISendGridClient>();
            _sendGridClientFactoryMock = new Mock<ISendGridClientFactory>();
            _sendGridClientFactoryMock.Setup(factory => factory.Create()).Returns(_sendGridClientMock.Object);

            _sendGridDetail = new SendGridDetail
            {
                ApiKey = "dummy_api_key",
                SenderEmail = "sender@example.com",
                SenderDisplayName = "Sender Name"
            };

            _sendGridEmailService = new SendGridEmailService(_sendGridDetail, _sendGridClientFactoryMock.Object);
        }

        [Fact]
        public async Task SendByTemplate_ShouldThrowArgumentNullException_WhenSubjectIsNullOrEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sendGridEmailService.SendByTemplate(null, "body", "recipient@example.com", new Dictionary<string, string>()));
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sendGridEmailService.SendByTemplate(string.Empty, "body", "recipient@example.com", new Dictionary<string, string>()));
        }

        [Fact]
        public async Task SendByTemplate_ShouldThrowArgumentNullException_WhenBodyIsNullOrEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sendGridEmailService.SendByTemplate("subject", null, "recipient@example.com", new Dictionary<string, string>()));
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sendGridEmailService.SendByTemplate("subject", string.Empty, "recipient@example.com", new Dictionary<string, string>()));
        }

        [Fact]
        public async Task SendByTemplate_ShouldThrowArgumentNullException_WhenRecipientIsNullOrEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sendGridEmailService.SendByTemplate("subject", "body", null, new Dictionary<string, string>()));
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sendGridEmailService.SendByTemplate("subject", "body", string.Empty, new Dictionary<string, string>()));
        }

        [Fact]
        public async Task SendByTemplate_ShouldSendEmailWithCorrectParameters()
        {
            var subject = "Test Subject";
            var body = "Test Body";
            var recipient = "recipient@example.com";
            var keywordData = new Dictionary<string, string> { { "Name", "John Doe" } };

            _sendGridClientMock
                .Setup(client => client.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response(HttpStatusCode.OK, null, null));

            await _sendGridEmailService.SendByTemplate(subject, body, recipient, keywordData);

            _sendGridClientMock.Verify(
                client => client.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task SendByTemplate_ShouldThrowException_WhenEmailSendFails()
        {
            var subject = "Test Subject";
            var body = "Test Body";
            var recipient = "recipient@example.com";
            var keywordData = new Dictionary<string, string>();

            _sendGridClientMock
                .Setup(client => client.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response(HttpStatusCode.BadRequest, null, null));

            var exception = await Assert.ThrowsAsync<Exception>(() => _sendGridEmailService.SendByTemplate(subject, body, recipient, keywordData));
            Assert.Contains(Constants.SendEmailFailed, exception.Message);
        }
    }
}
