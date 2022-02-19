using EmailSender.Application.DTOs;
using EmailSender.Application.Interfaces;
using EmailSender.Infrastructure.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace EmailSender.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailService(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration.Value;
        }

        public async Task Send(SendDTO sendDTO)
        {
            using (var client = new SmtpClient())
            {
                client.Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
                await client.ConnectAsync(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.Auto);

                await client.AuthenticateAsync(sendDTO.Credential.EmailAddress, sendDTO.Credential.Password);

                if (client.IsAuthenticated && client.IsConnected)
                {
                    foreach (var item in sendDTO.MimeMessages)
                    {
                        try
                        {
                            await client.SendAsync(item);
                        }
                        catch (Exception)
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
