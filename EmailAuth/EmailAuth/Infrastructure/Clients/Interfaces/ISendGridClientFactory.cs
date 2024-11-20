using SendGrid;

namespace EmailAuth.Infrastructure.Clients.Interfaces
{
    internal interface ISendGridClientFactory
    {
        ISendGridClient Create();
    }
}
