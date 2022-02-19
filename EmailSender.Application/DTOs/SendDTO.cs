using MimeKit;

namespace EmailSender.Application.DTOs
{
    public class SendDTO
    {
        public IList<MimeMessage> MimeMessages { get; set; }
        public CredentialDTO Credential { get; set; }
    }
}
