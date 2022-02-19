using EmailSender.Application.DTOs;
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
        private readonly IAesService _aesService;

        public SendEmailCommandHandler(
            IEmailService emailService,
            IMimeMessageService mimeMessageService,
            ILogger<SendEmailCommandHandler> logger,
            IAesService aesService)
        {
            _emailService = emailService;
            _mimeMessageService = mimeMessageService;
            _logger = logger;
            _aesService = aesService;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var decryptedCredentials = new CredentialDTO
            {
                EmailAddress = _aesService.DecryptStringFromBytes(request.Credential.EmailAddress),
                Password = _aesService.DecryptStringFromBytes(request.Credential.Password),
            };

            var mimeMessages = await _mimeMessageService.GetMessagesAsync(new GetMessagesAsyncDTO
            {
                EmailTemplate = request.EmailTemplate,
                Subject = request.Subject,
                Credential = decryptedCredentials,
                Receivers = request.Receivers,
                From = request.From,
            });

            await _emailService.Send(new SendDTO
            {
                MimeMessages = mimeMessages,
                Credential = decryptedCredentials
            });

            return Unit.Value;
        }
    }
}
