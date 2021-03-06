using EmailSender.Application.DTOs;

namespace EmailSender.BindingModels
{
    public class SendEmailBindingModel
    {
        public IList<ReceiverDTO> Receivers { get; set; }
        public string EmailTemplate { get; set; }
        public string Subject { get; set; }
        public CredentialDTO Credential { get; set; }
        public string From { get; set; }
    }
}