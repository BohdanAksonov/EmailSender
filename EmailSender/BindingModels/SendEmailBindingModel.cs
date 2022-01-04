namespace EmailSender.BindingModels
{
    public class SendEmailBindingModel
    {
        public IList<Receivers> Receivers { get; set; }
        public string EmailTemplate { get; set; }
        public string Subject { get; set; }
    }

    public class Receivers
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}