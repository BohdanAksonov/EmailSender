namespace EmailSender.Application.CQRS.Email
{
    public class SendEmailCommandResult
    {
        public List<string> EmailAddresses { get; set; }
    }
}
