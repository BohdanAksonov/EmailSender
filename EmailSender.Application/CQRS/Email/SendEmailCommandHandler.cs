using EmailSender.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmailSender.Application.CQRS.Email
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly IMimeMessageService _mimeMessageService;
        private readonly ILogger<SendEmailCommandHandler> _logger;

        public SendEmailCommandHandler(
            IEmailService emailService,
            IMimeMessageService mimeMessageService,
            ILogger<SendEmailCommandHandler> logger)
        {
            _emailService = emailService;
            _mimeMessageService = mimeMessageService;
            _logger = logger;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var mimeMessages = await _mimeMessageService.GetMessagesAsync(request.Receivers, request.EmailTemplate, request.Subject);

            await _emailService.Send(mimeMessages);

            return Unit.Value;
        }
    }
}
