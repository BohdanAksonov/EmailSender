namespace EmailSender.Application.Interfaces
{
    public interface IAesService
    {
        string DecryptStringFromBytes(string encryptedValue);
    }
}
