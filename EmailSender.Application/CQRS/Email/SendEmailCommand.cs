using EmailSender.Application.DTOs;
using MediatR;

namespace EmailSender.Application.CQRS.Email
{
    public class SendEmailCommand : IRequest<Unit>
    {
        public IList<ReceiverDTO> Receivers { get; set; }
        public string EmailTemplate { get; set; }
        public string Subject { get; set; }
    }
}
