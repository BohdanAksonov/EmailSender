namespace EmailSender.Application.DTOs
{
    public class GetMessagesAsyncDTO
    {
        public IList<ReceiverDTO> Receivers { get; set; }
        public string EmailTemplate { get; set; }
        public string Subject { get; set; }
        public CredentialDTO Credential { get; set; }
        public string From { get; set; }
    }
}
