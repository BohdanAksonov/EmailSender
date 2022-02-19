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

        public async Task<IList<MimeMessage>> GetMessagesAsync(GetMessagesAsyncDTO param)
        {
            var result = new List<MimeMessage>();

            foreach (var item in param.Receivers)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(param.From, param.Credential.EmailAddress));
                message.To.Add(new MailboxAddress(item.Name, item.EmailAddress));
                message.Subject = param.Subject;

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = param.EmailTemplate.Replace("{FullName}", item.Name),
                };

                result.Add(message);
            }

            return result;
        }
    }
}
