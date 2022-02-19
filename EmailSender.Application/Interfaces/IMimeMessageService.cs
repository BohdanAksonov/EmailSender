using EmailSender.Application.DTOs;
using MimeKit;

namespace EmailSender.Application.Interfaces
{
    public interface IMimeMessageService
    {
        Task<IList<MimeMessage>> GetMessagesAsync(GetMessagesAsyncDTO param);
    }
}
