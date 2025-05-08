using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Project_ASP.NET.SMTP;

namespace Project_ASP.NET.Interfaces
{
    public interface ISMTPService
    {
        Task<bool> SendEmail(MessageModel message);
    }
}
