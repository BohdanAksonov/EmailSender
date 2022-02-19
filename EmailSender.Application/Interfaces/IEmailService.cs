using EmailSender.Application.DTOs;

namespace EmailSender.Application.Interfaces
{
    public interface IEmailService
    {
        Task Send(SendDTO sendDTO);
    }
}
