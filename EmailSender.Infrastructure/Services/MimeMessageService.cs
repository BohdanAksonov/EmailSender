using EmailSender.Application.DTOs;
using EmailSender.Application.Interfaces;
using EmailSender.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace EmailSender.Infrastructure.Services
{
    public class MimeMessageService : IMimeMessageService
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public MimeMessageService(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration.Value;
        }

        public async Task<IList<MimeMessage>> GetMessagesAsync(IList<ReceiverDTO> receivers, string emailTemplate, string subject)
        {
            var result = new List<MimeMessage>();

            foreach (var item in receivers)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpConfiguration.From, _smtpConfiguration.Username));
                message.To.Add(new MailboxAddress(item.Name, item.EmailAddress));
                message.Subject = subject;

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailTemplate.Replace("{FullName}", item.Name),
                };

                result.Add(message);
            }

            return result;
        }
    }
}
