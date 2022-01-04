using MimeKit;

namespace EmailSender.Application.Interfaces
{
    public interface IEmailService
    {
        Task Send(IList<MimeMessage> mimeMessages);
    }
}
