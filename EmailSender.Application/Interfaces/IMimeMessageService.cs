using EmailSender.Application.DTOs;
using MimeKit;

namespace EmailSender.Application.Interfaces
{
    public interface IMimeMessageService
    {
        Task<IList<MimeMessage>> GetMessagesAsync(IList<ReceiverDTO> receivers, string emailTemplate, string subject);
    }
}
