using EmailSender.Application.Interfaces;
using EmailSender.Infrastructure.Configurations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace EmailSender.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailService(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration.Value;
        }

        public async Task Send(IList<MimeMessage> mimeMessages)
        {
            using (var client = new SmtpClient())
            {
                client.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
                await client.ConnectAsync(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.Auto);

                await client.AuthenticateAsync(_smtpConfiguration.Username, _smtpConfiguration.Password);

                if (client.IsAuthenticated && client.IsConnected)
                {
                    foreach (var item in mimeMessages)
                    {
                        try
                        {
                            await client.SendAsync(item);
                        }
                        catch (Exception ex)
                        {

                            continue;
                        }
                        
                    }
                }

                await client.DisconnectAsync(true);
            }
        }
    }
}
