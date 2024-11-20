using EmailAuth.Infrastructure.Clients.Interfaces;
using SendGrid;

namespace EmailAuth.Infrastructure.Clients
{
    internal class SendGridClientFactory: ISendGridClientFactory
    {
        private readonly string _apiKey;

        public SendGridClientFactory(string apiKey)
        {
            _apiKey = apiKey;
        }

        public ISendGridClient Create()
        {
            return new SendGridClient(_apiKey);
        }
    }
}
